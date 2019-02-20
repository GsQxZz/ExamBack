using Exam.Back.IRespository.Log;
using Exam.Back.IRespository.Setting;
using Exam.Back.Model;
using Exam.Back.Mvc.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Exam.Back.Mvc.Controllers
{
  
    public class AdministratorController : Controller
    {
        IUserRespository user;
        ILogRespository logs;
        public AdministratorController(IUserRespository userRespository, ILogRespository log)
        {
            user = userRespository;
            logs = log;
        }

        /// <summary>
        /// 用户后台登录
        /// </summary>
        /// <returns></returns>

        [AllowAnonymous]
        public ActionResult DahinterLogin()
        {
            return View();
        }

        [HttpPost]
        public int Login(string AccountName, string Pwd, string yz)
        {
            if (Session["SecurityCode"].ToString().ToUpper() != yz.ToUpper())
            {
                return -2;
            }

            List<Users> list = user.Login(AccountName, Pwd);
            if (list.Count > 0)
            {
                Session["username"] = AccountName;
                Session["User"] = list.FirstOrDefault();
                //在登录成功处判断
                if (Convert.ToInt32(list.FirstOrDefault().State) == 0)
                {
                    int id = list.FirstOrDefault().ID;
                    int a =  user.StateUpt(id);
                    logs.Add(list[0].ID, "登录", 1);
                    return a;
                }
                else
                {
                    logs.Add(list[0].ID, "登录", 0);
                    return -5;
                }
            }
            return 0;
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public ActionResult Quit()
        {
            //设置Session为null
            string name = Session["username"].ToString();
            int result = user.StateEdit(name);


            return RedirectToAction("DahinterLogin");
        }


        public ActionResult DahinterRegister()
        {
            return View();
        }
        public int Register(string AccountName, string Pwd, string Phone)
        {
            int i = user.Register(AccountName, Pwd, Phone);
            List<Users> list = user.Login(AccountName, Pwd);
            if (i > 0)
            {
                logs.Add(list[0].ID, "注册", 1);
            }
            else
            {
                logs.Add(list[0].ID, "注册", 0);
            }
            return i;
        }

        //获取短信验证码
        public ActionResult GetSMS(string phone, string sms)
        {
            var result = EtuoCloud.SendSmsCode(phone, 1, sms);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #region  生成验证码图片
        // [OutputCache(Location = OutputCacheLocation.None, Duration = 0, NoStore = false)]
        [AllowAnonymous]
        public ActionResult SecurityCode()
        {

            string oldcode = Session["SecurityCode"] as string;
            string code = CreateRandomCode(5);
            Session["SecurityCode"] = code;
            return File(CreateValidateGraphic(code), "image/Jpeg");
        }


        private byte[] CreateImage(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 12);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 20);
            Graphics g = Graphics.FromImage(image);
            Font f = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            Brush b = new System.Drawing.SolidBrush(Color.White);
            g.Clear(Color.Blue);
            g.DrawString(checkCode, f, b, 3, 3);
            Pen blackPen = new Pen(Color.Black, 0);
            Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                int x1 = rand.Next(image.Width);
                int x2 = rand.Next(image.Width);
                int y1 = rand.Next(image.Height);
                int y2 = rand.Next(image.Height);
                g.DrawLine(new Pen(Color.Silver, 1), x1, y1, x2, y2);
            }
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        private string CreateRandomCode(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(35);
                if (temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }
        /// <summary>
        /// 创建验证码的图片
        /// </summary>
        public byte[] CreateValidateGraphic(string validateCode)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 16.0), 27);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的干扰线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 13, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                 Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);
                //画图片的前景干扰点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
        #endregion

        // GET: Administrator
        /// <summary>
        /// 权限页面
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get|HttpVerbs.Post)]
        public ActionResult PermissionShow(string name)
        {
            List<Permissions> list = user.GetPermissions(name);
            return View(list);
        }


        /// <summary>
        /// 添加权限
        /// </summary>
        /// <returns></returns>s
        public ActionResult PermissionInsert()
        {
            return View();
        }
        public int PermissionAdd(Permissions p)
        {
            int i = user.PermissionsAdd(p);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "添加权限", 1);
            }
            else
            {
                logs.Add(us.ID, "添加权限", 0);
            }
            return i;
        }

        /// <summary>
        /// 添加子级节点
        /// </summary>
        /// <returns></returns>
        public ActionResult AddZi()
        {
            return View();
        }
        [HttpPost]
        public int AddZi(Permissions p)
        {
            int i = user.AddZTree(p);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "添加子节点", 1);
            }
            else
            {
                logs.Add(us.ID, "添加子节点", 0);
            }
            return i;
        }

        /// <summary>
        /// (单)删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int PermissionDelete(int id)
        {
            int i = user.PermissionDel(id);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "单个删除权限", 1);
            }
            else
            {
                logs.Add(us.ID, "单个删除权限", 0);
            }
            return i;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int PermissionsDelete(string id)
        {
            int i = user.PermissionsDel(id);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "批量删除权限", 1);
            }
            else
            {
                logs.Add(us.ID, "批量删除权限", 0);
            }
            return i;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            return View();
        }

        /// <summary>
        /// 权限反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetPermissionByID(int id)
        {
            Permissions permissions = user.GetPermissionByID(id).FirstOrDefault();
            return JsonConvert.SerializeObject(permissions);
        }

        /// <summary>
        /// 权限修改
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int PermissionsUpt(Permissions p)
        {
            int i = user.PermissionsUpt(p);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "权限修改", 1);
            }
            else
            {
                logs.Add(us.ID, "权限修改", 0);
            }
            return i;
        }


        /// <summary>
        /// 角色显示
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult RolesShow()
        {
            List<Role> list = user.GetRoles();
            return View(list);
        }

        /// <summary>
        /// 角色添加
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleInsert()
        {
            List<Permissions> list = user.BingPermission();
            ViewBag.zc = list.Where(m => m.PID == 0).ToList();
            ViewBag.zr = list;
            return View(list);
        }

        public int RoleAdd(string RoleName, int Status, string Describe, string PerName)
        {
            Roles roles = new Roles();
            roles.RoleName = RoleName;
            roles.Status = Status;
            roles.Describe = Describe;
            int i = user.RoleAdd(roles);

            var a = PerName.Split(',');
            int roleId = user.GetRoleId();
            for (int j = 0; j < a.Length; j++)
            {
                RolePermissionRelation relation = new RolePermissionRelation();
                relation.RoleId = roleId;
                relation.PermissionId = int.Parse(a[j]);
                int result = user.RAPermissionAdd(relation);
            }
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "添加角色", 1);
            }
            else
            {
                logs.Add(us.ID, "添加角色", 0);
            }
            return i;

        }

        /// <summary>
        /// (单)删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public int RoleDelete(int id)
        {
               int i = user.RoleDel(id);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "单个删除角色", 1);
            }
            else
            {
                logs.Add(us.ID, "单个删除角色", 0);
            }
            return i;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int RolesDelete(string id)
        {
            int i = user.RolesDel(id);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "批量删除角色", 1);
            }
            else
            {
                logs.Add(us.ID, "批量删除角色", 0);
            }
            return i;
        }

        /// <summary>
        /// 角色修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditRole(int id)
        {
            List<Permissions> list = user.BingPermission();
            ViewBag.zc = list.Where(m => m.PID == 0).ToList();
            ViewBag.zr = list;
            Role role = user.RoleByID(id).FirstOrDefault();
            return View(role);
        }
        public int RoleUpt(Roles r)
        {
            int i = user.RoleUpt(r);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "角色修改", 1);
            }
            else
            {
                logs.Add(us.ID, "角色修改", 0);
            }
            return i;
        }
        /// <summary>
        ///角色反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetRoleByID(int id)
        {
            Role roles = user.RoleByID(id).FirstOrDefault();
            return JsonConvert.SerializeObject(roles);
        }


        /// <summary>
        /// 用户显示查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        
        public ActionResult UserShow(string name)
        {
            List<User> list = user.GetUsers(name);
            return View(list);
        }

        /// <summary>
        /// 用户添加
        /// </summary>
        /// <returns></returns>
        public ActionResult UserAdd()
        {
            List<Roles> list = user.BingRoles();
            ViewBag.zr = list;
            return View();
        }   
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public int UserInsert(string AccountName,string Pwd,string Sex,string Phone,DateTime CreTime,string OwnRole)
        {
            Users u = new Users();
            u.AccountName = AccountName;
            u.Pwd = Pwd;
            u.Sex = Sex;
            u.Phone = Phone;
            u.CreTime = CreTime;
            int i = user.UserAdd(u);

            var a = OwnRole.Split(',');
            int userid = user.GetUserId();
            for (int j = 0; j < a.Length; j++)  
            {
                UserRoleRelation relation = new UserRoleRelation ();
                relation.UserId = userid;
                relation.RoleId = int.Parse(a[j]);
                int result = user.RARolesAdd(relation);
            }
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "添加用户", 1);
            }
            else
            {
                logs.Add(us.ID, "添加用户", 0);
            }
            return i;
        }
        /// <summary>
        /// (单)删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public int UserDelete(int id)
        {
            int i = user.UserDel(id);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "单个删除用户", 1);
            }
            else
            {
                logs.Add(us.ID, "单个删除用户", 0);
            }
            return i;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public int UsersDel(string id)
        {
            int i = user.UsersDel(id);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "批量删除用户", 1);
            }
            else
            {
                logs.Add(us.ID, "批量删除用户", 0);
            }
            return i;
        }
        /// <summary>
        /// 用户修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditUser(int id)
        {
            List<Roles> list = user.BingRoles();
            ViewBag.zr = list;
            Users users = user.UserByID(id).FirstOrDefault();
            return View(users);
        }
        public int UserUpt(Users s)
        {
            int i = user.UserUpt(s);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "修改用户信息", 1);
            }
            else
            {
                logs.Add(us.ID, "修改用户信息", 0);
            }
            return i;
        }
        /// <summary>
        ///角色反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetUserByID(int id)
        {
            Users users= user.UserByID(id).FirstOrDefault();
            return JsonConvert.SerializeObject(users);
        }
        /// <summary>
        /// 添加角色权限（修改角色）
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="PermissionId"></param>
        /// <returns></returns>
        public int AddRolePer(int RoleId, int PermissionId)
        {
            int i = user.AddRolePer(RoleId, PermissionId);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "添加角色权限", 1);
            }
            else
            {
                logs.Add(us.ID, "添加角色权限", 0);
            }
            return i;
        }
        /// <summary>
        /// 删除角色权限（修改角色）
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="PermissionId"></param>
        /// <returns></returns>
        public int DeleteRolePer(int RoleId, int PermissionId)
        {
            int i = user.DeleteRolePer(RoleId, PermissionId);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "删除角色权限", 1);
            }
            else
            {
                logs.Add(us.ID, "删除角色权限", 0);
            }
            return i;
        }
    }
}