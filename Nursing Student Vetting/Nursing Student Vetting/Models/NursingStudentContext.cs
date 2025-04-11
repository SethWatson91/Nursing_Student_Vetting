
using Nursing_Student_Vetting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;



public class NursingStudentContext : IdentityDbContext<IdentityUser>
{
    public NursingStudentContext(DbContextOptions<NursingStudentContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<StudentTest> StudentTests { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<ClassCategories> ClassCategories { get; set; }
    public DbSet<StudentClass> StudentClasses { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StudentTest>()
            .HasKey(st => new { st.TestID, st.AttemptNumber, st.StudentID });

        modelBuilder.Entity<StudentClass>()
            .HasKey(sc => new { sc.ClassID, sc.CategoryPrefix, sc.StudentID });

        modelBuilder.Entity<StudentTest>()
            .HasOne(st => st.Test)
            .WithMany(t => t.StudentTests)
            .HasForeignKey(st => st.TestID);

        modelBuilder.Entity<StudentTest>()
            .HasOne(st => st.Student)
            .WithMany(s => s.StudentTests)
            .HasForeignKey(st => st.StudentID);

        modelBuilder.Entity<StudentClass>()
            .HasOne(sc => sc.Student)
            .WithMany(s => s.StudentClasses)
            .HasForeignKey(sc => sc.StudentID);

        modelBuilder.Entity<StudentClass>()
            .HasOne(sc => sc.Class)
            .WithMany(c => c.StudentClasses)
            .HasForeignKey(sc => new { sc.ClassID, sc.CategoryPrefix });

        modelBuilder.Entity<Class>()
            .HasKey(c => new { c.ClassID, c.CategoryPrefix }); // Composite key

        modelBuilder.Entity<Class>()
            .HasOne(c => c.Category)
            .WithMany(cc => cc.Classes)
            .HasForeignKey(c => c.CategoryPrefix)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Student>()
            .Property(s => s.StudentID)
            .ValueGeneratedNever();

        // Seed data for ClassCategories
        modelBuilder.Entity<ClassCategories>().HasData(
            new ClassCategories { CategoryName = "Accounting", CategoryPrefix = "ACCT" },
            new ClassCategories { CategoryName = "Agriculture", CategoryPrefix = "AGRI" },
            new ClassCategories { CategoryName = "Agriculture", CategoryPrefix = "AGRM" },
            new ClassCategories { CategoryName = "Anthropology", CategoryPrefix = "ANTH" },
            new ClassCategories { CategoryName = "Art", CategoryPrefix = "ART" },
            new ClassCategories { CategoryName = "Art Performance", CategoryPrefix = "ARTP" },
            new ClassCategories { CategoryName = "Astronomy", CategoryPrefix = "ASTR" },
            new ClassCategories { CategoryName = "Biology", CategoryPrefix = "BIOL" },
            new ClassCategories { CategoryName = "Business", CategoryPrefix = "BUSN" },
            new ClassCategories { CategoryName = "Chemistry", CategoryPrefix = "CHEM" },
            new ClassCategories { CategoryName = "Communications", CategoryPrefix = "COMM" },
            new ClassCategories { CategoryName = "Computer Info Tech", CategoryPrefix = "CITC" },
            new ClassCategories { CategoryName = "Computer Science", CategoryPrefix = "CISP" },
            new ClassCategories { CategoryName = "Criminal Justice", CategoryPrefix = "CRMJ" },
            new ClassCategories { CategoryName = "Culinary Arts", CategoryPrefix = "CULA" },
            new ClassCategories { CategoryName = "Digital Media", CategoryPrefix = "DIGM" },
            new ClassCategories { CategoryName = "Early Childhood Education", CategoryPrefix = "ECED" },
            new ClassCategories { CategoryName = "Economics", CategoryPrefix = "ECON" },
            new ClassCategories { CategoryName = "Education", CategoryPrefix = "EDUC" },
            new ClassCategories { CategoryName = "Electrical Engin Tech", CategoryPrefix = "EETC" },
            new ClassCategories { CategoryName = "Emergency Med Serv Para", CategoryPrefix = "EMSP" },
            new ClassCategories { CategoryName = "Emergency Med Service", CategoryPrefix = "EMSA" },
            new ClassCategories { CategoryName = "Emergency Med Service", CategoryPrefix = "EMSB" },
            new ClassCategories { CategoryName = "Engineering", CategoryPrefix = "ENGR" },
            new ClassCategories { CategoryName = "Engineering Systems Tech", CategoryPrefix = "ENST" },
            new ClassCategories { CategoryName = "Engineering Technology", CategoryPrefix = "EGRT" },
            new ClassCategories { CategoryName = "English", CategoryPrefix = "ENGL" },
            new ClassCategories { CategoryName = "Fire Science", CategoryPrefix = "FIRE" },
            new ClassCategories { CategoryName = "French", CategoryPrefix = "FREN" },
            new ClassCategories { CategoryName = "Geography", CategoryPrefix = "GEOG" },
            new ClassCategories { CategoryName = "Geology", CategoryPrefix = "GEOL" },
            new ClassCategories { CategoryName = "Health", CategoryPrefix = "HLTH" },
            new ClassCategories { CategoryName = "Health Information Management", CategoryPrefix = "HIMT" },
            new ClassCategories { CategoryName = "History", CategoryPrefix = "HIST" },
            new ClassCategories { CategoryName = "Hospitality Management", CategoryPrefix = "HGMT" },
            new ClassCategories { CategoryName = "Humanities", CategoryPrefix = "HUM" },
            new ClassCategories { CategoryName = "Information Systems", CategoryPrefix = "INFS" },
            new ClassCategories { CategoryName = "Mathematics", CategoryPrefix = "MATH" },
            new ClassCategories { CategoryName = "Music", CategoryPrefix = "MUS" },
            new ClassCategories { CategoryName = "Nursing", CategoryPrefix = "NRSG" },
            new ClassCategories { CategoryName = "Occupational Thrpy Asst", CategoryPrefix = "OTAP" },
            new ClassCategories { CategoryName = "Paralegal", CategoryPrefix = "LEGL" },
            new ClassCategories { CategoryName = "Pharmacy Technician", CategoryPrefix = "PHRX" },
            new ClassCategories { CategoryName = "Philosophy", CategoryPrefix = "PHIL" },
            new ClassCategories { CategoryName = "Physical Education", CategoryPrefix = "PHED" },
            new ClassCategories { CategoryName = "Physical Science", CategoryPrefix = "PSCI" },
            new ClassCategories { CategoryName = "Physical Therapist Asst", CategoryPrefix = "PTAT" },
            new ClassCategories { CategoryName = "Physics", CategoryPrefix = "PHYS" },
            new ClassCategories { CategoryName = "Political Science", CategoryPrefix = "POLS" },
            new ClassCategories { CategoryName = "Psychology", CategoryPrefix = "PSYC" },
            new ClassCategories { CategoryName = "Reading", CategoryPrefix = "READ" },
            new ClassCategories { CategoryName = "Religion", CategoryPrefix = "RELS" },
            new ClassCategories { CategoryName = "Respiratory Care", CategoryPrefix = "RESP" },
            new ClassCategories { CategoryName = "Social Work", CategoryPrefix = "SWRK" },
            new ClassCategories { CategoryName = "Sociology", CategoryPrefix = "SOCI" },
            new ClassCategories { CategoryName = "Spanish", CategoryPrefix = "SPAN" },
            new ClassCategories { CategoryName = "Special Education", CategoryPrefix = "SPED" },
            new ClassCategories { CategoryName = "Surgical Technology", CategoryPrefix = "SURG" },
            new ClassCategories { CategoryName = "Theatre", CategoryPrefix = "THEA" },
            new ClassCategories { CategoryName = "Women/Gender Studies", CategoryPrefix = "WGST" }

        );

        // Seed data for Classes
        modelBuilder.Entity<Class>().HasData(
                new Class { ClassID = 1010, ClassName = "Principles of Accounting I", CreditHours = 3, CategoryPrefix = "ACCT", IsRequired = false },
                new Class { ClassID = 1010, ClassName = "English Composition ", CreditHours = 3, CategoryPrefix = "ENGL", IsRequired = true },
                new Class { ClassID = 1010, ClassName = "Early Humanities", CreditHours = 3, CategoryPrefix = "HUM", IsRequired = false },
                new Class { ClassID = 1010, ClassName = "Computer Applications", CreditHours = 3, CategoryPrefix = "INFS", IsRequired = true },
                new Class { ClassID = 1020, ClassName = "Modern Humanities", CreditHours = 3, CategoryPrefix = "HUM", IsRequired = false },
                new Class { ClassID = 1030, ClassName = "Introduction to Music", CreditHours = 3, CategoryPrefix = "MUS", IsRequired = false },
                new Class { ClassID = 1030, ClassName = "Introduction to Philosophy", CreditHours = 3, CategoryPrefix = "PHIL", IsRequired = false },
                new Class { ClassID = 1030, ClassName = "Introduction to Psychology ", CreditHours = 3, CategoryPrefix = "PSYC", IsRequired = true },
                new Class { ClassID = 1030, ClassName = "Civilizations and Cultures of the Spanish-speaking World", CreditHours = 3, CategoryPrefix = "SPAN", IsRequired = false },
                new Class { ClassID = 1030, ClassName = "Introduction to Theatre", CreditHours = 3, CategoryPrefix = "THEA", IsRequired = false },
                new Class { ClassID = 1035, ClassName = "Introduction to Art", CreditHours = 3, CategoryPrefix = "ART", IsRequired = false },
                new Class { ClassID = 1040, ClassName = "Introduction to Ethics", CreditHours = 3, CategoryPrefix = "PHIL", IsRequired = false },
                new Class { ClassID = 1530, ClassName = "Introductory Statistics", CreditHours = 3, CategoryPrefix = "MATH", IsRequired = true },
                new Class { ClassID = 2000, ClassName = "Art History Survey I", CreditHours = 3, CategoryPrefix = "ART", IsRequired = false },
                new Class { ClassID = 2010, ClassName = "Human Anatomy and Physiology I", CreditHours = 3, CategoryPrefix = "BIOL", IsRequired = true },
                new Class { ClassID = 2011, ClassName = "Human Anatomy and Physiology I Lab", CreditHours = 1, CategoryPrefix = "BIOL", IsRequired = true },
                new Class { ClassID = 2020, ClassName = "Art History Survey II", CreditHours = 3, CategoryPrefix = "ART", IsRequired = false },
                new Class { ClassID = 2020, ClassName = "Human Anatomy and Physiology II", CreditHours = 3, CategoryPrefix = "BIOL", IsRequired = true },
                new Class { ClassID = 2020, ClassName = "Technology and Society", CreditHours = 3, CategoryPrefix = "EGRT", IsRequired = false },
                new Class { ClassID = 2020, ClassName = "Self & Values", CreditHours = 3, CategoryPrefix = "PHIL", IsRequired = false },
                new Class { ClassID = 2020, ClassName = "Religions of the World", CreditHours = 3, CategoryPrefix = "RELS", IsRequired = false },
                new Class { ClassID = 2021, ClassName = "Human Anatomy and Physiology II Lab", CreditHours = 1, CategoryPrefix = "BIOL", IsRequired = true },
                new Class { ClassID = 2025, ClassName = "Fundamentals of Communication", CreditHours = 3, CategoryPrefix = "COMM", IsRequired = true },
                new Class { ClassID = 2110, ClassName = "Early American Literature", CreditHours = 3, CategoryPrefix = "ENGL", IsRequired = false },
                new Class { ClassID = 2120, ClassName = "Modern American Literature", CreditHours = 3, CategoryPrefix = "ENGL", IsRequired = false },
                new Class { ClassID = 2200, ClassName = "Introduction to World Religions", CreditHours = 3, CategoryPrefix = "PHIL", IsRequired = false },
                new Class { ClassID = 2210, ClassName = "Modern American Literature", CreditHours = 3, CategoryPrefix = "ENGL", IsRequired = false },
                new Class { ClassID = 2220, ClassName = "Modern British Literature", CreditHours = 3, CategoryPrefix = "ENGL", IsRequired = false },
                new Class { ClassID = 2230, ClassName = "Introduction to Microbiology", CreditHours = 3, CategoryPrefix = "BIOL", IsRequired = true },
                new Class { ClassID = 2231, ClassName = "Introduction to Microbiology Lab", CreditHours = 1, CategoryPrefix = "BIOL", IsRequired = true },
                new Class { ClassID = 2410, ClassName = "Early European Literature", CreditHours = 3, CategoryPrefix = "ENGL", IsRequired = false },
                new Class { ClassID = 2420, ClassName = "Modern European Literature", CreditHours = 3, CategoryPrefix = "ENGL", IsRequired = false },
                new Class { ClassID = 2640, ClassName = "Science and the Modern World", CreditHours = 3, CategoryPrefix = "PHIL", IsRequired = false },
                new Class { ClassID = 2860, ClassName = "Introductory Film Studies", CreditHours = 3, CategoryPrefix = "HUM", IsRequired = false }
        );

        // Seed data for Students
        modelBuilder.Entity<Student>().HasData(
            new Student { StudentID = "W00001001", FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Address = "123 Example St", DateOfBirth = new DateTime(2000, 1, 1), Gender = "Male", StartDate = new DateTime(2020, 8, 1), GraduationDate = new DateTime(2024, 5, 15) },
            new Student { StudentID = "W00001002", FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Address = "456 Example Ave", DateOfBirth = new DateTime(1999, 5, 15), Gender = "Female", StartDate = new DateTime(2021, 1, 5) },
            new Student { StudentID = "W00001234", FirstName = "Seth", LastName = "Watson", Email = "seth.watson91@gmail.com", Address = "657 Example Ave", DateOfBirth = new DateTime(1990, 4, 12), Gender = "Male", StartDate = new DateTime(2021, 1, 5) }
            );

        // Seed data for Tests
        modelBuilder.Entity<Test>().HasData(
            new Test { TestID = 1, TestName = "ACT", GradingScale = 36 },
            new Test { TestID = 2, TestName = "Designated Test", GradingScale = 100 }
        );

        // Seed data for StudentTests
        modelBuilder.Entity<StudentTest>().HasData(

            new StudentTest { TestID = 1, AttemptNumber = 1, StudentID = "W00001001", Score = 22 },
            new StudentTest { TestID = 2, AttemptNumber = 1, StudentID = "W00001001", Score = 74 },
            new StudentTest { TestID = 2, AttemptNumber = 2, StudentID = "W00001002", Score = 94 },
            new StudentTest { TestID = 2, AttemptNumber = 1, StudentID = "W00001002", Score = 92 }
        );

        // Seed data for StudentClasses
        modelBuilder.Entity<StudentClass>().HasData(
            new StudentClass { ClassID = 1010, CategoryPrefix = "ACCT", StudentID = "W00001001", LetterGrade = "A" },
            new StudentClass { ClassID = 1010, CategoryPrefix = "ACCT", StudentID = "W00001002", LetterGrade = "A" },
            new StudentClass { ClassID = 1010, CategoryPrefix = "ACCT", StudentID = "W00001234", LetterGrade = "A" },
            new StudentClass { ClassID = 1010, CategoryPrefix = "ENGL", StudentID = "W00001234", LetterGrade = "A" },
            new StudentClass { ClassID = 1010, CategoryPrefix = "INFS", StudentID = "W00001234", LetterGrade = "A" },
            new StudentClass { ClassID = 1030, CategoryPrefix = "PSYC", StudentID = "W00001234", LetterGrade = "A" },
            new StudentClass { ClassID = 1530, CategoryPrefix = "MATH", StudentID = "W00001234", LetterGrade = "A" },
            new StudentClass { ClassID = 2010, CategoryPrefix = "BIOL", StudentID = "W00001001", LetterGrade = "B" },
            new StudentClass { ClassID = 2010, CategoryPrefix = "BIOL", StudentID = "W00001002", LetterGrade = "C" },
            new StudentClass { ClassID = 2010, CategoryPrefix = "BIOL", StudentID = "W00001234", LetterGrade = "A" },
            new StudentClass { ClassID = 2011, CategoryPrefix = "BIOL", StudentID = "W00001234", LetterGrade = "B" },
            new StudentClass { ClassID = 2020, CategoryPrefix = "ART", StudentID = "W00001234", LetterGrade = "A" },
            new StudentClass { ClassID = 2020, CategoryPrefix = "BIOL", StudentID = "W00001234", LetterGrade = "A" },
            new StudentClass { ClassID = 2021, CategoryPrefix = "BIOL", StudentID = "W00001234", LetterGrade = "A" },
            new StudentClass { ClassID = 2025, CategoryPrefix = "COMM", StudentID = "W00001234", LetterGrade = "A" },
            new StudentClass { ClassID = 2230, CategoryPrefix = "BIOL", StudentID = "W00001234", LetterGrade = "A" },
            new StudentClass { ClassID = 2231, CategoryPrefix = "BIOL", StudentID = "W00001234", LetterGrade = "A" }
        );
    }
}

