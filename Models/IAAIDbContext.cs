using System;
using System.Data.Entity;
using System.Linq;

namespace IAAI.Models
{
    public class IAAIDbContext : DbContext
    {
        // 您的內容已設定為使用應用程式組態檔 (App.config 或 Web.config)
        // 中的 'IAAIDbContext' 連接字串。根據預設，這個連接字串的目標是
        // 您的 LocalDb 執行個體上的 'IAAI.Models.IAAIDbContext' 資料庫。
        // 
        // 如果您的目標是其他資料庫和 (或) 提供者，請修改
        // 應用程式組態檔中的 'IAAIDbContext' 連接字串。
        public IAAIDbContext()
            : base("name=IAAIDbContext")
        {
        }

        // 針對您要包含在模型中的每種實體類型新增 DbSet。如需有關設定和使用
        // Code First 模型的詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=390109。

        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<AboutUs> About { get; set; }

        public virtual DbSet<CertifiedMember> Certified { get; set; }

        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<Organization> Organizations { get; set; }

        public virtual DbSet<History> Histories { get; set; }

        public virtual DbSet<News> News { get; set; }

        public System.Data.Entity.DbSet<IAAI.Models.NewsCatalog> NewsCatalogs { get; set; }

        public virtual DbSet<Master> Masters { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}