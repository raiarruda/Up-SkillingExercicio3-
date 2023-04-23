using Dapper;
using SistemaEstacionamento.Data;
using SistemaEstacionamento.Model;

namespace SistemaEstacionamento.Repository
{
    public interface IEstacionamentoRepository
    {
        Cliente BuscaClientePorId(int id);
        Cliente GetByCpf(string cpf);
        List<Cliente> ListaClientes();
        List<Veiculo> ListVeiculos();
        void InserirCliente(Cliente cliente);
        void InserirVeiculo(Veiculo veiculo);
        Veiculo BuscaVeiculoPorPlaca(string placa);
    }
    public class EstacionamentoRepository: IEstacionamentoRepository
    {

        private readonly IDatabaseConfig _databaseConfig;

        public EstacionamentoRepository(IDatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public Cliente BuscaClientePorId(int id)
        {
            using (var connection = _databaseConfig.CreateConnection())
            {
                connection.Open();
                var sql = "SELECT * FROM cliente WHERE Id = @Id";
                return connection.QueryFirstOrDefault<Cliente>(sql, new { Id = id });
            }
        }

        public Cliente GetByCpf(string cpf)
        {
            using (var connection = _databaseConfig.CreateConnection())
            {
                connection.Open();
                var sql = "SELECT * FROM cliente WHERE cpf = @cpf";

                return connection.QueryFirstOrDefault<Cliente>(sql, new { cpf = cpf });

            }
        }
        public List<Cliente> ListaClientes()
        {
            using (var connection = _databaseConfig.CreateConnection())
            {
                connection.Open();
                var sql = "SELECT * FROM cliente";

                return connection.Query<Cliente>(sql).ToList();

            }
        }
        public void InserirCliente(Cliente cliente)
        {
            using var connection = _databaseConfig.CreateConnection();
            connection.Open();

            var sql = @"insert into cliente(nome, cpf)
            values(@nome, @cpf);";


            var id = connection.ExecuteScalar<int>(sql, cliente);
            cliente.Id = id;
        }

        public void InserirVeiculo(Veiculo veiculo)
        {
            using var connection = _databaseConfig.CreateConnection();
            connection.Open();

            var sql = @"insert into veiculo(id_cliente, marca, modelo, placa) 
            values (@IdCliente, @Marca, @Modelo, @Placa)";

            var id = connection.ExecuteScalar<int>(sql, veiculo);
        }

        public List<Veiculo> ListVeiculos()
        {
            using (var connection = _databaseConfig.CreateConnection())
            {
                connection.Open();
                var sql = "SELECT * FROM veiculo v " +
                    "inner join cliente c on v.id_cliente = c.id";

                return connection.Query<Veiculo>(sql).ToList();

            }
        }

        public Veiculo BuscaVeiculoPorPlaca(string placa)
        {
            using (var connection = _databaseConfig.CreateConnection())
            {
                connection.Open();
                var sql = "SELECT * FROM veiculo where placa = @placa";

                return connection.QueryFirstOrDefault<Veiculo>(sql, new { placa = placa });

            }
        }
    }
}
