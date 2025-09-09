using Microsoft.EntityFrameworkCore;

[Keyless]
public class MonitoringHoursCosts
{
    public int ContractID { get; set; }

    public decimal PrevistoHH { get; set; }
    public double PlanejadoHH { get; set; }
    public double ExecutadoHH { get; set; }
    public double? DiferencaHH { get; set; }

    public decimal PrevistoRS { get; set; }
    public decimal PlanejadoRS { get; set; }
    public decimal ExecutadoRS { get; set; }
    public decimal? DiferencaRS { get; set; }
}
