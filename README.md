# SimpleMigrator
Simple Migrator

# This is a light migrator engine with a new feature we call it safe migration!

# Sample
	this.Table("MigrationsHistory")
                .CreateIfNotExist(i => {
                    i.AddColumn("Id", SqlDbType.Int, allowNull: false, identity: new Identity(1, 1));
                    i.AddColumn("Version","nvarchar(10)", allowNull: false);
                    i.AddColumn("Date","nvarchar(10)", allowNull: false);
                });
