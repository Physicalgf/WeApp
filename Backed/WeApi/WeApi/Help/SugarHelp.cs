using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WeApi.Help
{
    public class SugarHelp
    {
        #region sugar
        //创建数据库对象 (用法和EF Dappper一样通过new保证线程安全)
        public static SqlSugarClient Db = new SqlSugarClient(new ConnectionConfig()
        {

            //ConnectionString = "server=localhost;port=3306;user=root;password=sll.0411;database=rootadmin",
            ConnectionString = "server=121.40.185.118;port=3306;user=rootadmin;password=sll.0411;database=sys",
            DbType = DbType.MySql,
            IsAutoCloseConnection = true
        }
        ,
        db =>
        {

            db.Aop.OnLogExecuting = (sql, pars) =>
            {

                //获取原生SQL推荐 5.1.4.63  性能OK
                Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));

                //获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
                //Console.WriteLine(UtilMethods.GetSqlString(DbType.SqlServer,sql,pars))


            };

            //注意多租户 有几个设置几个
            //db.GetConnection(i).Aop

        }
          );

        #region 获取数据列表
        /// <summary>
        /// 根据条件获取实体列列表
        /// </summary>
        /// <param name="expression">筛选的条件  如果等于null  就查询全部</param>
        /// <returns></returns>
        public static List<T> GetList<T>(Expression<Func<T, bool>> expression)
        {
            if (expression != null)
            {
                return Db.Queryable<T>().Where(expression).ToList();
            }
            else
            {
                return Db.Queryable<T>().ToList();
            }

        }
        #endregion

        #region 新增数据
        /// <summary>
        /// 新增单个数据
        /// </summary>
        /// <typeparam name="T">新增的类</typeparam>
        /// <param name="entity">新增的数据</param>
        /// <param name="ignoreColumns">不新增指定的列</param>
        /// <returns>返回新增的主键值</returns>
        public static int Insert<T>(T entity, params string[] ignoreColumns)
            where T : class, new()
        {
            // 执行新增
            return Db.Insertable(entity)
                .IgnoreColumns(ignoreColumns)
                .ExecuteReturnIdentity();
        }

        /// <summary>
        /// 新增多个数据
        /// </summary>
        /// <typeparam name="T">新增的类</typeparam>
        /// <param name="entity">新增的数据</param>
        /// <param name="ignoreColumns">不新增指定的列</param>
        /// <returns>返回最后新增的主键值</returns>
        public static int Insert<T>(List<T> entity, params string[] ignoreColumns)
            where T : class, new()
        {
            // 执行新增
            return Db.Insertable(entity)
                .IgnoreColumns(ignoreColumns)
                .ExecuteReturnIdentity();
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnsExpression">实例化对象，赋值多少列就修改多少列</param>
        /// <param name="whereExpression">修改的条件 相当于sql的 where</param>
        /// <returns>返回修改成功的条数</returns>
        public static int Update<T>(Expression<Func<T, T>> columnsExpression, Expression<Func<T, bool>> whereExpression)
            where T : class, new()
        {
            // 执行有条件的更新
            return Db.Updateable<T>()
                .SetColumns(columnsExpression)
                .Where(whereExpression)
                .ExecuteCommand();
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">要修改的类参数</param>
        /// <param name="whereColumnsExpression">修改的条件 相当于sql的 where</param>
        /// <returns>返回修改成功的行数</returns>
        public static int Update<T>(T entity, Expression<Func<T, object>> whereColumnsExpression) where T : class, new()
        {
            // 执行更新
            return Db.Updateable(entity)
                .WhereColumns(whereColumnsExpression)
                .ExecuteCommand();
        }

        #endregion

        #region 查看详情
        /// <summary>
        /// 查看详情
        /// </summary>
        /// <typeparam name="T">要查看的实体类</typeparam>
        /// <param name="expression">条件</param>
        /// <returns></returns>
        public static T GetEntity<T>(Expression<Func<T, bool>> expression)
        {
            return Db.Queryable<T>().First(expression);
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T">要删除的实体类</typeparam>
        /// <param name="whereExpression">删除的条件</param>
        /// <returns></returns>
        public static int Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereExpression) where T : class, new()
        {
            return Db.Deleteable<T>().Where(whereExpression).ExecuteCommand();
        }
        #endregion

        #region 示例
        //    List<SupplyUserClicks> supplyUserClickList = new List<SupplyUserClicks>(0);

        //    SupplyUserClicks supplyUserClicks = new SupplyUserClicks();
        //    supplyUserClicks.SupplyOfGoodsId = 666;
        //    supplyUserClicks.Createtime = DateTime.Now;
        //    supplyUserClicks.ClickCount = 2;
        //    supplyUserClicks.User_Mac = "新增数据1";

        //    SupplyUserClicks supplyUserClick2 = new SupplyUserClicks();
        //    supplyUserClick2.SupplyOfGoodsId = 666;
        //    supplyUserClick2.Createtime = DateTime.Now;
        //    supplyUserClick2.ClickCount = 2;
        //    supplyUserClick2.User_Mac = "新增数据2";

        //    supplyUserClickList.Add(supplyUserClicks);
        //    supplyUserClickList.Add(supplyUserClick2);

        //    #region 新增
        //    //新增单个
        //    //var insets = SugarHelp.Insert(supplyUserClicks, nameof(SupplyUserClicks.Id));

        //    //新增多个
        //    //var insetList = SugarHelp.Insert(supplyUserClickList, nameof(SupplyUserClicks.Id));
        //    #endregion

        //    #region 修改
        //    //通过实例化类修改
        //    SugarHelp.Update<SupplyUserClicks>(t => new SupplyUserClicks() { SupplyOfGoodsId = 5, User_Mac = "修改成功" }, t => t.Id == 4);

        //    //通过变量修改
        //    SugarHelp.Update<SupplyUserClicks>(supplyUserClicks, t => t.Id == 4);
        //    #endregion

        //    #region 获取详情
        //    SugarHelp.GetEntity<SupplyUserClicks>(t=>t.Id==Id)
        //    #endregion

        //    #region 删除
        //    SugarHelp.Delete<SupplyUserClicks>(t => t.Id == 1);
        //    #endregion

        //    #region 获取列表
        ////  获取有条件的列表
        //    SugarHelp.GetEntity<SupplyUserClicks>(t=>t.Id==Id);

        //    //获取全部
        //    SugarHelp.GetEntity<SupplyUserClicks>(null);

        //    #endregion
        #endregion
        #endregion

        #region 实体类转换
        /// <summary>
        ///  从 TIn 实体转换到 TOut实体，
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="ti"></param>
        /// <param name="to"></param>
        public static void Trans<TIn, TOut>(TIn ti, TOut to)
        {
            to.GetType().GetProperties().ToList().ForEach(t =>
            {
                var pro = ti.GetType().GetProperties().Where(p => p.Name == t.Name).FirstOrDefault();
                if (pro != null)
                {
                    var value = pro.GetValue(ti);
                    if (value != DBNull.Value)
                    {
                        t.SetValue(to, value, null);
                    }
                }
            });
        }
        #endregion
    }
}
