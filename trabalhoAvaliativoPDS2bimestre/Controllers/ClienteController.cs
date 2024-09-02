using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trabalhoAvaliativoPDS2bimestre.cliente;

namespace trabalhoAvaliativoPDS2bimestre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        List<Cliente> listacliente = new List<Cliente>();

        public ClienteController()
        {
            var cliente1= new Cliente();
            var cliente2= new Cliente();
            var cliente3= new Cliente();
            listacliente.Add(cliente1);
            listacliente.Add(cliente2);
            listacliente.Add(cliente3);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Cliente item)
        {
            var cliente = new Cliente();
            cliente.Telofone = item.Telofone;
            cliente.DataNascimento = item.DataNascimento;
            cliente.Estado = item.Estado;
            cliente.Cidade = item.Cidade;
            cliente.Id = item.Id;
            cliente.Cpf = item.Cpf;
            cliente.Email = item.Email;
            cliente.Endereço = item.Email;
            cliente.Rg = item.Rg;
            cliente.Nome = item.Nome;
            cliente.Sexo = item.Sexo;
            listacliente.Add(cliente);
            return StatusCode(StatusCodes.Status201Created, cliente);

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(listacliente);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cliente = listacliente.Where(item => item.Id == id).FirstOrDefault();

            return Ok(cliente);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Cliente item)
        {
            var cliente = listacliente.Where(item => item.Id == id).FirstOrDefault();
            if (cliente == null)
            {
                return NotFound();
            }

            cliente.Nome = item.Nome;
            cliente.Cpf = item.Cpf;

            return Ok(listacliente);
        }

        [HttpDelete("{id}")]
        public IActionResult Delet(int id)
        {
            var cliente = listacliente.Where(item => item.Id == id).FirstOrDefault();
            if (cliente == null)
            {
                return NotFound();
            }

            listacliente.Remove(cliente);
            return Ok(cliente);
        }

        public static class CertificacaoCPF
        {
            public static bool ValidacaoCPF(string cpf)
            {
                cpf = cpf.Replace(".", "");
                cpf = cpf.Replace("-", "");
                if (cpf.Length > 11 || cpf.Length < 11)
                {
                    return false;
                }

                int[] multi = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                int multiplicador = 0;

                for (int i = 0; i < cpf.Length - 2; i++)
                {
                    multiplicador += Convert.ToInt32(cpf[i].ToString()) * multi[i];
                }
                int numero1 = 0;
                if (multiplicador % 11 < 2)
                {
                    numero1 = 0;
                }
                else if (multiplicador % 11 >= 2)
                {
                    numero1 = 11 - (multiplicador % 11);
                }


                int[] multi2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                int multiplicador2 = 0;

                for (int i = 0; i < cpf.Length - 1; i++)
                {
                    multiplicador2 += Convert.ToInt32(cpf[i].ToString()) * multi2[i];
                }

                int numero2 = 0;

                if (multiplicador2 % 11 < 2)
                {
                    numero2 = 0;
                }

                else if (multiplicador2 % 11 >= 2)
                {
                    numero2 = 11 - (multiplicador2 % 11);
                }

                if (Convert.ToInt32(cpf[9].ToString()) == numero1 && Convert.ToInt32(cpf[10].ToString()) == numero2)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

        }

    }
}
