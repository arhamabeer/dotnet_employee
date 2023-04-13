namespace dotnet_mvc.Models
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
               new Employee
               {
                   name = "Arham",
                   department = Dept.IT,
                   email = "arham@gmail.com",
                   id = 1,
                   photoPath = null
               },
                 new Employee
                 {
                     name = "Abeer",
                     department = Dept.Design,
                     email = "arham@abeer.com",
                     photoPath = null,
                     id = 2,
                 },
                   new Employee
                   {
                       name = "Ahmed",
                       department = Dept.None,
                       email = "ahmed@aaa.com",
                       photoPath = null,
                       id = 3,
                   }
               );
        }
    }
}
