using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    class CareerCloudContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyProfilePoco>()
            .Ignore(t => t.TimeStamp)
            .HasMany(c => c.CompanyDescriptions)
            .WithRequired(d => d.CompanyProfiles)
            .HasForeignKey(d => d.Company)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyProfilePoco>()
            .Ignore(t => t.TimeStamp)
            .HasMany(l => l.CompanyLocations)
            .WithRequired(p => p.CompanyProfile)
            .HasForeignKey(d => d.Company)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyProfilePoco>()
            .Ignore(t => t.TimeStamp)
            .HasMany(d => d.CompanyJobs)
            .WithRequired(p => p.CompanyProfile)
           .HasForeignKey(d => d.Company)
           .WillCascadeOnDelete(true);


            modelBuilder.Entity<SystemLanguageCodePoco>()
             .HasMany(d => d.CompanyDescriptions)
             .WithRequired(p => p.SystemLanguageCode)
             .HasForeignKey(d => d.LanguageId)
             .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyJobPoco>()
            .Ignore(t => t.TimeStamp)
           .HasMany(d => d.CompanyJobEducations)
           .WithRequired(p => p.CompanyJob)
           .HasForeignKey(d => d.Job)
           .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyJobPoco>()
            .Ignore(t => t.TimeStamp)
           .HasMany(d => d.CompanyJobDescriptions)
           .WithRequired(p => p.CompanyJob)
           .HasForeignKey(d => d.Job)
           .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyJobPoco>()
            .Ignore(t => t.TimeStamp)
           .HasMany(d => d.ApplicantJobApplications)
           .WithRequired(p => p.CompanyJob)
           .HasForeignKey(d => d.Applicant)
           .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyJobPoco>()
            .Ignore(t => t.TimeStamp)
           .HasMany(d => d.CompanyJobSkills)
           .WithRequired(p => p.CompanyJob)
           .HasForeignKey(d => d.Job)
           .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyProfilePoco>()
            .Ignore(t => t.TimeStamp)
            .HasMany(d => d.CompanyJobs)
            .WithRequired(p => p.CompanyProfile)
            .HasForeignKey(d => d.Company)
            .WillCascadeOnDelete(true);


            modelBuilder.Entity<ApplicantProfilePoco>()
            .Ignore(t => t.TimeStamp)
           .HasMany(a => a.ApplicantJobApplications)
           .WithRequired(p => p.ApplicantProfile)
           .HasForeignKey(a => a.Applicant)
           .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicantProfilePoco>()
             .Ignore(t => t.TimeStamp)
            .HasMany(e => e.ApplicantEducations)
            .WithRequired(p => p.ApplicantProfile)
            .HasForeignKey(a => a.Applicant)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicantProfilePoco>()
            .Ignore(t => t.TimeStamp)
            .HasMany(r => r.ApplicantResumes)
            .WithRequired(p => p.ApplicantProfile)
            .HasForeignKey(a => a.Applicant)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicantProfilePoco>()
            .Ignore(t => t.TimeStamp)
           .HasMany(s => s.ApplicantSkills)
           .WithRequired(p => p.ApplicantProfile)
           .HasForeignKey(a => a.Applicant)
           .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicantProfilePoco>()
            .Ignore(t => t.TimeStamp)
            .HasMany(w => w.ApplicantWorkHistories)
            .WithRequired(p => p.ApplicantProfile)
            .HasForeignKey(a => a.Applicant)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<SystemCountryCodePoco>()
           .HasMany(w => w.ApplicantProfiles)
           .WithRequired(p => p.SystemCountryCode)
           .HasForeignKey(a => a.Country)
           .WillCascadeOnDelete(true);

            modelBuilder.Entity<SecurityLoginPoco>()
            .Ignore(t => t.TimeStamp)
          .HasMany(w => w.ApplicantProfiles)
          .WithRequired(p => p.SecurityLogin)
          .HasForeignKey(a => a.Login)
          .WillCascadeOnDelete(true);

            modelBuilder.Entity<SecurityLoginPoco>()
            .Ignore(t => t.TimeStamp)
          .HasMany(s => s.SecurityLoginsLogs)
          .WithRequired(p => p.SecurityLogin)
          .HasForeignKey(a => a.Login)
          .WillCascadeOnDelete(true);

            modelBuilder.Entity<SecurityLoginPoco>()
              .Ignore(t => t.TimeStamp)
            .HasMany(s => s.SecurityLoginsRoles)
            .WithRequired(p => p.SecurityLogin)
             .HasForeignKey(a => a.Login)
             .WillCascadeOnDelete(true);

            modelBuilder.Entity<SecurityRolePoco>()
            .HasMany(s => s.SecurityLoginsRoles)
            .WithRequired(p => p.SecurityRole)
            .HasForeignKey(p => p.Role)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<SystemCountryCodePoco>()
          .HasMany(s => s.ApplicantWorkHistories)
          .WithRequired(p => p.SystemCountryCode)
          .HasForeignKey(p => p.CountryCode)
          .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }

        public CareerCloudContext() :
            base(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString)
        {

        }
        DbSet<ApplicantEducationPoco> applicantEducations { get; set; }
        DbSet<ApplicantJobApplicationPoco> applicantJobApplications { get; set; }
        DbSet<ApplicantProfilePoco> applicantProfiles { get; set; }
        DbSet<ApplicantResumePoco> applicantResumes { get; set; }
        DbSet<ApplicantSkillPoco> applicantSkills { get; set; }
        DbSet<ApplicantWorkHistoryPoco> applicantWorkHistories { get; set; }
        DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        DbSet<CompanyJobEducationPoco> companyJobEducations { get; set; }
        DbSet<CompanyJobSkillPoco> companyJobSkills { get; set; }
        DbSet<CompanyJobPoco> companyJobs { get; set; }
        DbSet<CompanyJobDescriptionPoco> companyJobDescriptions { get; set; }
        DbSet<CompanyLocationPoco> companyLocations { get; set; }
        DbSet<CompanyProfilePoco> companyProfiles { get; set; }
        DbSet<SecurityLoginPoco> securityLogins { get; set; }
        DbSet<SecurityLoginsLogPoco> securityLoginsLogs { get; set; }
        DbSet<SecurityLoginsRolePoco> securityLoginsRoles { get; set; }
        DbSet<SecurityRolePoco> securityRoles { get; set; }
        DbSet<SystemCountryCodePoco> systemCountryCodes { get; set; }
        DbSet<SystemLanguageCodePoco> systemLanguageCodes { get; set; }


    }
}
