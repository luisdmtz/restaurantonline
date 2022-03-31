using Dapper;
using InduccionSemana4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace InduccionSemana4.Controllers
{
    public class SesionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Vendedor _vendedor)
        {
            string sql = "SELECT * FROM Vendedores WHERE Correo = '" + _vendedor.Correo + "' AND Pwd = '" + _vendedor.Pwd + "'; ";
            
            //Cambiar la base de datos aqui si existe posible error ... OJO AQUI
            using (var connection = new SqlConnection("Data Source= MEX-J6TNMG3\\SQLEXPRESS; Initial Catalog= RestaurantOnline; Trusted_Connection= True; MultipleActiveResultSets= True "))
            {
                
                var vendedor = connection.Query<Vendedor>(sql).Where(x => x.Correo == _vendedor.Correo && x.Pwd == _vendedor.Pwd);
                if (vendedor != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }


            }
        }
    }
}
