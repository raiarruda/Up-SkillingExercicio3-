namespace SistemaEstacionamento.Model
{
    public class EntradaSaida
    {
        public string PlacaVeiculo { get; set; }
        public DateTime HorarioEntrada { get; set; }
        public DateTime? HorarioSaida { get; set; }
    }
}
