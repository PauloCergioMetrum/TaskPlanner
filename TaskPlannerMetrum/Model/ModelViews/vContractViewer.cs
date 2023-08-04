using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vContractViewer
    {
        [Key]
        public int financeid { get; set; }
        public int cID { get; set; }
        public string RESPONSÁVEL { get; set; }

        public string EMPRESA { get; set; }

        public DateTime DATA { get; set; }

        public string SENIOR { get; set; }

        public string CLIENTE { get; set; }

        [Column("DESCRICAO DA VENDA")]
        public string DESCRICAODAVENDA { get; set; }

        public string VENDEDOR { get; set; }


        [Column("QNT.")]

        public string QNT { get; set; }

        [Column("VALOR PV")]
        public double VALORPV { get; set; }

        [Column("VALOR FATURADO")]
        public double VALOR_FATURADO { get; set; }

        [Column("DATA BASE")]
        public DateTime DATA_BASE { get; set; }

        [Column("DATA REPROGRAMADA")]

        public DateTime DATA_REPROGRAMADA { get; set; }

        [Column("MES PREVISTO FATURAMENTO")]

        public string MES_PREVISTO_FATURAMENTO { get; set; }

        [Column("FORMA DE PAGAMENTO")]

        public string FORMA_DE_PAGAMENTO { get; set; }

        [Column("COND. PG")]

        public string COND_PG { get; set; }

        [Column("FATURADO DIA")]

        public DateTime FATURADO_DIA { get; set; }

        [Column("NOTA FISCAL")]

        public string NOTA_FISCAL { get; set; }


        [Column("SETOR RESPONSAVEL")]

        public string SETOR_RESPONSAVEL { get; set; }

        public string TIPO { get; set; }



        [Column("STATUS PV")]

        public string STATUS_PV { get; set; }


        [Column("Status Faturamento")]

        public string Status_Faturamento { get; set; }  
 
     

    }
}
