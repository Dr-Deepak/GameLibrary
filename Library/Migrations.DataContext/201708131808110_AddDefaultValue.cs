namespace Library.Migrations.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    
    public partial class AddDefaultValue : DbMigration
    {
        public override void Up()
        {
            List<string> tables = new List<string>
            {
                "Rating",
                "Game",
                "GameGenre",
                "Genre"
            };
            foreach (var table in tables)
            {
                string tablename = String.Format("dbo.{0}s", table);
                string primerykey = String.Format("{0}Id", table);

                AlterColumn("dbo.Ratings", "RatingId", x => x.String(nullable: false, maxLength: 128, defaultValueSql: "newid()"));
                AlterColumn("dbo.Ratings", "CreateDate", x => x.DateTime(nullable: false, defaultValueSql: "getutcdate()"));
                AlterColumn("dbo.Ratings", "EditDate", x => x.DateTime(nullable: false, defaultValueSql: "getutcdate()"));
            }
        }

        public override void Down()
        {
        }
    }
}
