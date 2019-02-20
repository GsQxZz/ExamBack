using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Back.Common;
using Exam.Back.IRespository.Setting;
using Exam.Back.Model;
using MySql.Data.MySqlClient;
using Dapper;
using System.Configuration;

namespace Exam.Back.Respository.Setting
{
    public class UserRespository : IUserRespository
    {
        string mysqlConnectionStr = ConfigurationManager.ConnectionStrings["group5"].ToString();
        /// <summary>
        /// 根据用户名查询所有的权限
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public List<Permissions> ManyPermission(string accountname)
        {
            List<Permissions> per = DapperHelper.GetList<Permissions>(string.Format("select DISTINCT e.Id,e.`Name`,e.Url,e.State,e.PId from users a join userrolerelation b on a.Id=b.UserId join roles c on b.RoleId=c.RId join rolepermissionrelation d on c.RId=d.RoleId join permissions e on d.PermissionId=e.Id where a.AccountName='{0}'", accountname));
            return per;
        }

        /// <summary>
        /// 后台注册用户
        /// </summary>
        /// <param name="AccountName"></param>
        /// <param name="Pwd"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int Register(string AccountName, string Pwd, string Phone)
        {
            int a   = DapperHelper.ExecuteNonQuery(string.Format("insert into Users values('{0}','{1}','{2}')",AccountName,Pwd,Phone));
            return a;
        }
        /// <summary>
        /// 后台登录
        /// </summary>
        /// <param name="AccountName"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        public List<Users> Login(string AccountName, string Pwd)
        {
            List<Users> users = DapperHelper.GetList<Users>(string.Format("select * from Users where AccountName ='{0}' and Pwd = '{1}'", AccountName, Pwd));
            return users;
        }

        /// <summary>
        /// 通过修改数据库状态判断登录1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int StateUpt(int id)
        {
            string sql = string.Format("update Users  set State = 1 where ID = '{0}' ", id);
            int a = DapperHelper.ExecuteNonQuery(sql);
            return a;
        }
        /// <summary>
        /// 通过修改数据库状态判断登录2
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int StateEdit(string name)
        {
            string sql = string.Format("update Users set State =0 where AccountName ='{0}'", name);
            int b = DapperHelper.ExecuteNonQuery(sql);
            return b;
        }
        /// <summary>
        /// 权限反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Permissions> GetPermissionByID(int id)
        {
            List<Permissions> permissionlist = DapperHelper.GetList<Permissions>(string.Format("select * from Permissions where Id='{0}'", id));
            return permissionlist;
        }

        /// <summary>
        /// 权限显示
        /// </summary>
        /// <returns></returns>
        public List<Permissions> GetPermissions(string name)
        {
            string sql = "select * from Permissions where 1=1 and Name like '%" + name + "%' ";
            List<Permissions> permissionlist = DapperHelper.GetList<Permissions>(sql);
            return permissionlist;
        }

        /// <summary>
        /// 权限删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int PermissionDel(int id)
        {
            int i = DapperHelper.Delete<Permissions>(id);
            return i;
        }

        /// <summary>
        /// 绑定复选框
        /// </summary>
        public  List<Permissions> BingPermission()
        {
            string sql = "select * from Permissions";
            List<Permissions> list = DapperHelper.GetList<Permissions>(sql);
            return list;
        }

        /// <summary>
        /// 权限添加
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int PermissionsAdd(Permissions p)
        {
            int a = DapperHelper.Insert<Permissions>(p);
            return a;
        }

        /// <summary>
        /// 添加子节点(传父节点PId)
        /// </summary>
        /// <returns></returns>
        public int AddZTree(Permissions p)
        {
            int a = DapperHelper.Insert<Permissions>(p);
            return a;
        }
        /// <summary>
        /// 权限批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int PermissionsDel(string id)
        {
            int i = DapperHelper.Delete<Permissions>(id);
            return i;
        }
        /// <summary>
        /// 权限修改
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int PermissionsUpt(Permissions p)
        {
            int b = DapperHelper.Update<Permissions>(p);
            return b;
        }

        /// <summary>
        /// 角色显示
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRoles()
        {
            List<Role> list = DapperHelper.GetList<Role>("select a.RId,a.RoleName,a.Status,a.Describe,GROUP_CONCAT(c.`Name`) as PermissionName from roles a left join rolepermissionrelation b on a.RId=b.RoleId left join permissions c on b.PermissionId=c.Id GROUP BY a.RoleName");
            return list;
        }
        /// <summary>
        /// 角色添加
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int RoleAdd(Roles r)
        {
            int i = DapperHelper.ExecuteNonQuery(string.Format("insert into roles (RoleName,`Status`,`Describe`) values('{0}','{1}','{2}')", r.RoleName, r.Status, r.Describe));
            return i;
        }

        /// <summary>
        /// 获取角色Id
        /// </summary>
        /// <returns></returns>
       public  int GetRoleId()
        {
            string strSql = "select RId from Roles order by Rid DESC limit 1";
            int roleId = DapperHelper.GetList<Roles>(strSql).ToList().FirstOrDefault().RId;
            return roleId;
        }

        /// <summary>
        /// 角色与权限关系添加
        /// </summary>
        /// <param name="rolePermissionRelation"></param>
        /// <returns></returns>
        public int RAPermissionAdd(RolePermissionRelation rolePermissionRelation)
        {
            int result = DapperHelper.Insert<RolePermissionRelation>(rolePermissionRelation);
            return result;
        }

        /// <summary>
        /// 角色删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int RoleDel(int id)
        {
            int i = DapperHelper.Delete<Roles>(id);
            return i;
        }

        /// <summary>
        /// 角色批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int RolesDel(string id)
        {
            int i = DapperHelper.Delete<Roles>(id);
            return i;
        }

        /// <summary>
        /// 角色反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Role> RoleByID(int id)
        {
            List<Role> roles = DapperHelper.GetList<Role>(string.Format("select a.RId,a.RoleName,a.Status,a.Describe,GROUP_CONCAT(b.PermissionId)as PermissionName from roles a left join rolepermissionrelation b on a.RId=b.RoleId left join permissions c on b.PermissionId=c.Id GROUP BY a.RoleName HAVING a.RId={0}",id));
            return roles;
        }

        /// <summary>
        /// 角色修改
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int RoleUpt(Roles p)
        {
            string strSql = string.Format("update Roles set RoleName='{0}',`Status`='{1}',`Describe`='{2}' where RId={3} ", p.RoleName, p.Status, p.Describe, p.RId);
            int b = DapperHelper.ExecuteNonQuery(strSql);
            return b;
        }


        /// <summary>
        /// 用户显示和查询(分页后加)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public  List<User> GetUsers(string name)
        {
            string sql = string.Format("select a.*,GROUP_CONCAT(c.RoleName) as OwnRole from Users a left join userrolerelation b on a.Id=b.UserId left join roles c on b.RoleId=c.RId GROUP BY a.AccountName having 1=1 ");
            if (!string.IsNullOrEmpty(name))
            {
                sql += " and AccountName like '%" + name + "%'";
            }
            List<User> list = DapperHelper.GetList<User>(sql);
            return list;
        }

        /// <summary>
        /// 用户添加
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
      public  int UserAdd(Users s)
        {
            int i = DapperHelper.Insert<Users>(s);
            return i;
        }
        /// <summary>
        /// 用户删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         public int UserDel(int id)
        {
            int i = DapperHelper.Delete<Users>(id);
            return i;
        }
        /// <summary>
        /// 用户批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UsersDel(string id)
        {
            int i = DapperHelper.Delete<Users>(id);
            return i;
        }
        /// <summary>
        /// 用户反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Users> UserByID(int id)
        {
            List<Users> users = DapperHelper.GetList<Users>(string.Format("select * from Users where Id='{0}'", id));
            return users;
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
       public int UserUpt(Users u)
        {
            int b = DapperHelper.Update<Users>(u);
            return b;
        }

        public List<Roles> BingRoles()
        {
                string sql = "select * from Roles";
                List<Roles> list = DapperHelper.GetList<Roles>(sql);
                return list;
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <returns></returns>
        public int GetUserId()
        {
            //倒序取最后一条 刚添加进去的一条数据
            string strSql = "select ID from Users order by ID DESC limit 1";
            int userid = DapperHelper.GetList<Users>(strSql).ToList().FirstOrDefault().ID;
            return userid;
        }

        /// <summary>
        /// 用户与角色关系添加
        /// </summary>
        /// <param name="rolePermissionRelation"></param>
        /// <returns></returns>
        public int RARolesAdd(UserRoleRelation userRoleRelation)
        {
            int result = DapperHelper.Insert<UserRoleRelation>(userRoleRelation);
            return result;
        }
        /// <summary>
        /// 添加角色权限
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="PerId"></param>
        /// <returns></returns>
        public int AddRolePer(int RoleId, int PerId)
        {
            using (IDbConnection conn = new MySqlConnection(mysqlConnectionStr))
            {
                string sql = string.Format("insert into rolepermissionrelation values(Id,{0},{1})", RoleId, PerId);
                return conn.Execute(sql);
            }
        }
        /// <summary>
        /// 删除角色权限
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="PerId"></param>
        /// <returns></returns>
        public int DeleteRolePer(int RoleId, int PerId)
        {
            using (IDbConnection conn = new MySqlConnection(mysqlConnectionStr))
            {
                string sql = string.Format("DELETE from rolepermissionrelation where RoleId={0} and PermissionId={1}", RoleId, PerId);
                return conn.Execute(sql);
            }
        }
    }
}
