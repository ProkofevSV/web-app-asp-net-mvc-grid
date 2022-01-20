namespace ToDoGrid.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class INIT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DayOfWeeks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayOfWeekName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        PerformerId = c.Int(nullable: false),
                        Importance = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        Time = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Performers", t => t.PerformerId, cascadeDelete: true)
                .Index(t => t.PerformerId);
            
            CreateTable(
                "dbo.Performers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Sex = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PerformerImages",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Guid = c.Guid(nullable: false),
                        Data = c.Binary(nullable: false),
                        ContentType = c.String(maxLength: 100),
                        DateChanged = c.DateTime(),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Performers", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TaskDayOfWeeks",
                c => new
                    {
                        Task_Id = c.Int(nullable: false),
                        DayOfWeek_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Task_Id, t.DayOfWeek_Id })
                .ForeignKey("dbo.Tasks", t => t.Task_Id, cascadeDelete: true)
                .ForeignKey("dbo.DayOfWeeks", t => t.DayOfWeek_Id, cascadeDelete: true)
                .Index(t => t.Task_Id)
                .Index(t => t.DayOfWeek_Id);
            
            CreateTable(
                "dbo.TopicTasks",
                c => new
                    {
                        Topic_Id = c.Int(nullable: false),
                        Task_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Topic_Id, t.Task_Id })
                .ForeignKey("dbo.Topics", t => t.Topic_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tasks", t => t.Task_Id, cascadeDelete: true)
                .Index(t => t.Topic_Id)
                .Index(t => t.Task_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TopicTasks", "Task_Id", "dbo.Tasks");
            DropForeignKey("dbo.TopicTasks", "Topic_Id", "dbo.Topics");
            DropForeignKey("dbo.Tasks", "PerformerId", "dbo.Performers");
            DropForeignKey("dbo.PerformerImages", "Id", "dbo.Performers");
            DropForeignKey("dbo.TaskDayOfWeeks", "DayOfWeek_Id", "dbo.DayOfWeeks");
            DropForeignKey("dbo.TaskDayOfWeeks", "Task_Id", "dbo.Tasks");
            DropIndex("dbo.TopicTasks", new[] { "Task_Id" });
            DropIndex("dbo.TopicTasks", new[] { "Topic_Id" });
            DropIndex("dbo.TaskDayOfWeeks", new[] { "DayOfWeek_Id" });
            DropIndex("dbo.TaskDayOfWeeks", new[] { "Task_Id" });
            DropIndex("dbo.PerformerImages", new[] { "Id" });
            DropIndex("dbo.Tasks", new[] { "PerformerId" });
            DropTable("dbo.TopicTasks");
            DropTable("dbo.TaskDayOfWeeks");
            DropTable("dbo.Topics");
            DropTable("dbo.PerformerImages");
            DropTable("dbo.Performers");
            DropTable("dbo.Tasks");
            DropTable("dbo.DayOfWeeks");
        }
    }
}
