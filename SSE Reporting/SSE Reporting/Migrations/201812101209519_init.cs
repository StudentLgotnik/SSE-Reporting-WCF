namespace SSE_Reporting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        TimeOff = c.Double(nullable: false),
                        Sickness = c.Double(nullable: false),
                        ProjectId = c.Int(),
                        Role = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Messagee = c.String(),
                        EmployeeId = c.Int(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Company = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Activity = c.Int(nullable: false),
                        ProjectId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(),
                        Activity = c.Int(nullable: false),
                        StartHours = c.Time(nullable: false, precision: 7),
                        EndHours = c.Time(nullable: false, precision: 7),
                        Date = c.DateTime(nullable: false),
                        Commect = c.String(),
                        Project_Id = c.Int(),
                        Task_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .ForeignKey("dbo.Tasks", t => t.Task_Id)
                .Index(t => t.Project_Id)
                .Index(t => t.Task_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "Task_Id", "dbo.Tasks");
            DropForeignKey("dbo.Reports", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Employees", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Reports", new[] { "Task_Id" });
            DropIndex("dbo.Reports", new[] { "Project_Id" });
            DropIndex("dbo.Tasks", new[] { "ProjectId" });
            DropIndex("dbo.Employees", new[] { "ProjectId" });
            DropTable("dbo.Reports");
            DropTable("dbo.Tasks");
            DropTable("dbo.Projects");
            DropTable("dbo.Messages");
            DropTable("dbo.Employees");
        }
    }
}
