using Exam.Back.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Back.IRespository.Setting
{
    public interface IUserRespository
    {
        /// <summary>
        /// 查询所有权限
        /// </summary>
        /// <param name="accountname"></param>
        /// <returns></returns>
        List<Permissions> ManyPermission(string accountname);

        /// <summary>
        /// 后台登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        List<Users> Login(string AccountName, string Pwd);

        /// <summary>
        /// 通过修改数据库状态判断登录1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int StateUpt(int id);

        /// <summary>
        /// 通过修改数据库状态判断登录2
        /// </summary>
        /// <param name="id"></param>
        int StateEdit(string name);

        /// <summary>
        /// 后台注册
        /// </summary>
        /// <param name="AccountName"></param>
        /// <param name="Pwd"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        int Register(string AccountName, string Pwd, string Phone);

        /// <summary>
        /// 权限显示
        /// </summary>
        /// <returns></returns>
        List<Permissions> GetPermissions(string name);

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        int AddZTree(Permissions p);

        /// <summary>
        /// 绑定 复选框
        /// </summary>
        List<Permissions> BingPermission();
        /// <summary>
        /// 权限添加
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        int PermissionsAdd(Permissions p);
        /// <summary>
        /// 权限删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int PermissionDel(int id);
        /// <summary>
        /// 权限批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int PermissionsDel(string id);
        /// <summary>
        /// 权限反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Permissions> GetPermissionByID(int id);
        /// <summary>
        /// 权限修改
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        int PermissionsUpt(Permissions p);

        /// <summary>
        /// 角色显示
        /// </summary>
        /// <returns></returns>
        List<Role> GetRoles();

        /// <summary>
        /// 角色添加
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        int RoleAdd(Roles r);

        /// <summary>
        /// 获取角色Id
        /// </summary>
        /// <returns></returns>
        int GetRoleId();

        /// <summary>
        /// 角色与权限关系添加
        /// </summary>
        /// <param name="rolePermissionRelation"></param>
        /// <returns></returns>
        int RAPermissionAdd(RolePermissionRelation rolePermissionRelation);

        /// <summary>
        /// 角色删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int RoleDel(int id);

        /// <summary>
        /// 角色批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int RolesDel(string id);

        /// <summary>
        /// 角色反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Role> RoleByID(int id);

        /// <summary>
        /// 角色修改
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        int RoleUpt(Roles r);

        /// <summary>
        /// 用户查询
        /// </summary>
        /// <param name="time"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        List<User> GetUsers(string name);

        /// <summary>
        /// 用户添加
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        int UserAdd(Users s);
        /// <summary>
        /// 绑定角色复选框
        /// </summary>
        List<Roles> BingRoles();
        /// <summary>
        /// 用户删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int UserDel(int id);

        /// <summary>
        /// 用户批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int UsersDel(string id);

        /// <summary>
        /// 用户反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Users> UserByID(int id);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        int UserUpt(Users u);

        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <returns></returns>
        int GetUserId();


        /// <summary>
        /// 用户与角色添加的关系
        /// </summary>
        /// <param name="userRoleRelation"></param>
        /// <returns></returns>
        int RARolesAdd(UserRoleRelation userRoleRelation);
        /// <summary>
        /// 添加角色权限
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="PerId"></param>
        /// <returns></returns>
        int AddRolePer(int RoleId, int PerId);
        /// <summary>
        /// 删除角色权限
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="PerId"></param>
        /// <returns></returns>
        int DeleteRolePer(int RoleId, int PerId);
    }
}
