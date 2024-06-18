using Microsoft.EntityFrameworkCore;
using portal_backend.Entities;
using portal_backend.Enums;

namespace portal_backend;

public partial class RvipsContext : DbContext
{
    public RvipsContext()
    {
    }

    public RvipsContext(DbContextOptions<RvipsContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }
    
    public virtual DbSet<Email> Emails { get; set; }

    public virtual DbSet<Sponsor> Sponsors { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }
    
    public virtual DbSet<OutgoingEmail> OutgoingEmails { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var connectionString = configuration["SqlServer:ConnectionString"];
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pk");

            entity.ToTable("User", "dbo");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.CreationDate);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OrganizationId);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });


        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Project_pk");

            entity.ToTable("Project", "dbo");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.CreationDate);
            entity.Property(e => e.EndOfProjectDate);
            entity.Property(e => e.Description);
            entity.Property(e => e.OrganizationId);
            entity.Property(e => e.Title);

        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Organization_pk");

            entity.ToTable("Organization", "dbo");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ImapServer)
                .HasMaxLength(255)
                .IsUnicode();
            entity.Property(e => e.ImapServerPort)
                .ValueGeneratedNever();
            entity.Property(e => e.ImapServerUserName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ImapServerUserPassword)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SmtpServer)
                .HasMaxLength(255)
                .IsUnicode();
            entity.Property(e => e.SmtpServerPort)
                .ValueGeneratedNever();
            entity.Property(e => e.SmtpServerUserName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SmtpServerUserPassword)
                .HasMaxLength(255)
                .IsUnicode(false);

        });
        
        modelBuilder.Entity<Email>(entity =>
        {
            entity.HasKey(e => new {e.MessageId, e.OrganizationId}).HasName("EmailOrganization_pk");

            entity.ToTable("Email", "dbo");

            entity.Property(e => e.MessageId);
            entity.Property(e => e.OrganizationId);
            entity.Property(e => e.Date);
            entity.Property(e => e.Subject);
            entity.Property(e => e.FromEmail);
            entity.Property(e => e.FromName);
            entity.Property(e => e.HtmlBody);
            entity.Property(e => e.TextBody);
        });
        
        modelBuilder.Entity<Sponsor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Sponsor_pk");

            entity.ToTable("Sponsor", "dbo");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.OrganizationId);
            entity.Property(e => e.SponsorName);
            entity.Property(e => e.TelephoneNumber);
            entity.Property(e => e.Address);
            entity.Property(e => e.Description);
            entity.Property(e => e.Email);
        });
        
        modelBuilder.Entity<OutgoingEmail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OutgoingEmail_pk");

            entity.ToTable("OutgoingEmail", "dbo");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.OrganizationId);
            entity.Property(e => e.UserId);
            entity.Property(e => e.ReceiverEmail);
            entity.Property(e => e.Subject);
            entity.Property(e => e.Body);
            entity.Property(e => e.Date);
        });


        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasData(
                new Role { Id = 1, Name = "Admin", UserType = UserTypeEnum.Admin },
                new Role { Id = 2, Name = "User", UserType = UserTypeEnum.Regular }
            );
        });
        
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Comment_pk");

            entity.ToTable("Comment", "dbo");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();
            
            entity.Property(e => e.UserId);
            entity.Property(e => e.SponsorId);
            entity.Property(e => e.CommentText);
            entity.Property(e => e.Date);

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}