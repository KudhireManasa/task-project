using taskproject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace taskproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly DatabaseContext dbcontext;
        public TaskController(DatabaseContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        [Route("GetAllData")]

        public  async Task<IEnumerable<dbtask>> GetAlldata()
        {
         var result= await Task.Run(()=> dbcontext.dbtask.FromSqlRaw(@" exec getalldata").ToListAsync());
            return result;
            //return dbcontext.dbtask
            //    .FromSqlRaw<Task>("getalldata ")
            //    .ToListAsync();
        }
        [HttpGet]
        [Route("GetDataById")]
        public async Task<IEnumerable<dbtask>> GetRegistrationId(int? TASKID)
        {
            var param = new SqlParameter("@TASKID ", TASKID);

            var result = await Task.Run(() => dbcontext.dbtask
             .FromSqlRaw(@"exec SP_GETALLDATABYID @TASKID ", param).ToListAsync());
            return result;
        }
        [HttpPost]
        [Route("Insert")]
        public async Task<int> Insert(dbtask t)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@TaskCategory", t.TaskCategory));
            parameter.Add(new SqlParameter("@SelectModule", t.SelectModule));
            parameter.Add(new SqlParameter("@SelectName", t.SelectName));
            parameter.Add(new SqlParameter("@TaskID", t.TaskID));
            parameter.Add(new SqlParameter("@Title", t.Title));
            parameter.Add(new SqlParameter("@Description",t.Description));
            parameter.Add(new SqlParameter("@Priority", t.Priority));
            parameter.Add(new SqlParameter("@Status", t.Status));
            parameter.Add(new SqlParameter("@DueDate ", t.DueDate));
            parameter.Add(new SqlParameter("@DueTime", t.DueTime));
            parameter.Add(new SqlParameter("@ReminderDate", t.ReminderDate));
            parameter.Add(new SqlParameter("@ReminderTime", t.ReminderTime));
            parameter.Add(new SqlParameter("@Notify", t.Notify));
            parameter.Add(new SqlParameter("@AssignTo", t.AssignTo));
            var result = await Task.Run(() => dbcontext.Database
           .ExecuteSqlRawAsync(@"exec INSERTTASK @TaskCategory,@SelectModule, @SelectName, @TaskID, @Title, @Description, @Priority, @Status, @DueDate, @DueTime, @ReminderDate, @ReminderTime, @Notify, @AssignTo", parameter.ToArray()));
            return result;
        }
        [HttpPut]
        [Route("update")]
        public async Task<int> update(dbtask t)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@TaskID", t.TaskID));
            parameter.Add(new SqlParameter("@TaskCategory", t.TaskCategory));
            parameter.Add(new SqlParameter("@SelectModule", t.SelectModule));
            parameter.Add(new SqlParameter("@SelectName", t.SelectName));
            parameter.Add(new SqlParameter("@TaskID",t.TaskID));
            parameter.Add(new SqlParameter("@Title", t.Title));
            parameter.Add(new SqlParameter("@Description",t.Description));
            parameter.Add(new SqlParameter("@Priority",t.Priority));
            parameter.Add(new SqlParameter("@Status",t.Status));
            parameter.Add(new SqlParameter("@DueDate ",t.DueDate));
            parameter.Add(new SqlParameter("@DueTime",t.DueTime));
            parameter.Add(new SqlParameter("@ReminderDate",t.ReminderDate));
            parameter.Add(new SqlParameter("@ReminderTime", t.ReminderTime));
            parameter.Add(new SqlParameter("@Notify",t.Notify));
            parameter.Add(new SqlParameter("@AssignTo",t.AssignTo));
            var result = await Task.Run(() => dbcontext.Database
           .ExecuteSqlRawAsync(@"exec  UpdateTask @TaskID,@TaskCategory,@SelectModule, @SelectName,  @Title, @Description, @Priority, @Status, @DueDate, @DueTime, @ReminderDate, @ReminderTime, @Notify, @AssignTo", parameter.ToArray()));
            return result;
        }


        [HttpDelete]
        [Route("Delete")]
        public async Task<int> Delete(int TaskID)
        {
            return await Task.Run(() => dbcontext.Database.ExecuteSqlInterpolatedAsync($" DeleteTask {TaskID}"));
        }


    }
    
}
