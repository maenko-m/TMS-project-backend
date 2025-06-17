using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Infrastructure.Data
{
    public class TmsDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<TestCase> TestCases { get; set; }
        public DbSet<TestSuite> TestSuites { get; set; }
        public DbSet<TestStep> TestSteps { get; set; }
        public DbSet<TestRun> TestRuns { get; set; }
        public DbSet<TestRunTestCase> TestRunTestCases { get; set; }
        public DbSet<Defect> Defects { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<TestPlan> TestPlans { get; set; }
        public DbSet<TestPlanTestCase> TestPlanTestCases { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public TmsDbContext(DbContextOptions<TmsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TmsDbContext).Assembly);

            // User: уникальный email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // ProjectUser: связь проекта и пользователя
            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu => pu.Project)
                .WithMany(p => p.ProjectUsers)
                .HasForeignKey(pu => pu.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu => pu.User)
                .WithMany(u => u.ProjectUsers)
                .HasForeignKey(pu => pu.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // TestCase: связи с проектом, набором и создателем
            modelBuilder.Entity<TestCase>()
                .HasOne(t => t.Project)
                .WithMany(p => p.TestCases)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TestCase>()
                .HasOne(t => t.Suite)
                .WithMany(ts => ts.TestCases)
                .HasForeignKey(t => t.SuiteId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TestCase>()
                .HasOne(t => t.CreatedBy)
                .WithMany(u => u.CreatedTestCases)
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // TestSuite: иерархия и связь с проектом
            modelBuilder.Entity<TestSuite>()
                .HasOne(ts => ts.Project)
                .WithMany(p => p.TestSuites)
                .HasForeignKey(ts => ts.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // TestStep: иерархия и связь с тест-кейсом
            modelBuilder.Entity<TestStep>()
                .HasOne(ts => ts.TestCase)
                .WithMany(tc => tc.Steps)
                .HasForeignKey(ts => ts.TestCaseId)
                .OnDelete(DeleteBehavior.Cascade);

            // TestRun: связь с проектом, вехой и ответственным
            modelBuilder.Entity<TestRun>()
                .HasOne(tr => tr.Project)
                .WithMany(p => p.TestRuns)
                .HasForeignKey(tr => tr.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TestRun>()
                .HasOne(tr => tr.Milestone)
                .WithMany(m => m.TestRuns)
                .HasForeignKey(tr => tr.MilestoneId)
                .OnDelete(DeleteBehavior.Restrict);

            // TestRunTestCase: связь прогона, тест-кейса и исполнителя
            modelBuilder.Entity<TestRunTestCase>()
                .HasOne(trtc => trtc.TestRun)
                .WithMany(tr => tr.TestRunTestCases)
                .HasForeignKey(trtc => trtc.TestRunId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TestRunTestCase>()
                .HasOne(trtc => trtc.TestCase)
                .WithMany(tc => tc.TestRunTestCases)
                .HasForeignKey(trtc => trtc.TestCaseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Defect: связи с проектом, прогоном, тест-кейсом и создателем
            modelBuilder.Entity<Defect>()
                .HasOne(d => d.Project)
                .WithMany(p => p.Defects)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Defect>()
                .HasOne(d => d.TestRun)
                .WithMany(tr => tr.Defects)
                .HasForeignKey(d => d.TestRunId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Defect>()
                .HasOne(d => d.TestCase)
                .WithMany(tc => tc.Defects)
                .HasForeignKey(d => d.TestCaseId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Defect>()
                .HasOne(d => d.CreatedBy)
                .WithMany(u => u.CreatedDefects)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Attachment: связь с проектом и загрузившим пользователем
            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.Project)
                .WithMany(p => p.Attachments)
                .HasForeignKey(a => a.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.UploadedBy)
                .WithMany(u => u.UploadedAttachments)
                .HasForeignKey(a => a.UploadedById)
                .OnDelete(DeleteBehavior.Restrict);

            // TestPlan: связь с проектом и создателем
            modelBuilder.Entity<TestPlan>()
                .HasOne(tp => tp.Project)
                .WithMany(p => p.TestPlans)
                .HasForeignKey(tp => tp.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TestPlan>()
                .HasOne(tp => tp.CreatedBy)
                .WithMany(u => u.CreatedTestPlans)
                .HasForeignKey(tp => tp.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // TestPlanTestCase: связь плана и тест-кейса
            modelBuilder.Entity<TestPlanTestCase>()
                .HasOne(tptc => tptc.TestPlan)
                .WithMany(tp => tp.TestPlanTestCases)
                .HasForeignKey(tptc => tptc.TestPlanId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TestPlanTestCase>()
                .HasOne(tptc => tptc.TestCase)
                .WithMany(tc => tc.TestPlanTestCases)
                .HasForeignKey(tptc => tptc.TestCaseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Milestone: связь с проектом
            modelBuilder.Entity<Milestone>()
                .HasOne(m => m.Project)
                .WithMany(p => p.Milestones)
                .HasForeignKey(m => m.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);


            // Tag: уникальность имени в проекте
            modelBuilder.Entity<Tag>()
                .HasIndex(t => t.Name)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
    //dotnet ef database update --startup-project ../TmsSolution.Presentation
    //dotnet ef migrations add "final12" --startup-project ../TmsSolution.Presentation
}
