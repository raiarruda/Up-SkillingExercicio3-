namespace SistemaEstacionamento.Model
{
    public class Aluguel
    {
        public Guid Id { get; set; }
        public Guid Id_cliente { get; set; }
        public Guid Id_carro { get; set; }
        public DateTime Horario { get; set; }
    }
}
