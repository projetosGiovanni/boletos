using System;
using System.IO;
using System.Windows.Forms;
using BoletoNet;
using WinFormsBoletos.Dominio;

namespace WinFormsBoletos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnHTML_Click(object sender, EventArgs e)
        {
            new BoletoBancarioBuilder()
                .ParaCodigoBanco(1)
                .ComBoleto( RetornaBoletoPreenchido() )
                .ComMostrarCodigoCarteira(true)
                .GerarBoletoHtml();
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            new BoletoBancarioBuilder()
                .ParaCodigoBanco(1)
                .ComBoleto( RetornaBoletoPreenchido() )
                .ComMostrarCodigoCarteira(true)
                .GerarBoletoPdf();
        }

        private Boleto RetornaBoletoPreenchido() {

            DateTime vencimento = Convert.ToDateTime("04/07/2017"); 
            decimal valorBoleto = Convert.ToDecimal("19,24");
            string numeroDocumento = "501953"; 
            int cedente_convenio = 2815189; 
            string boleto_nossoNumero = "8757"; 

            #region informações pessoais do sacado / pagador         
            string sacado_cpfCnpj = "034.648.884-28";
            string sacado_nome = "GIOVANNI DE PAIVA NICOLETTI"; 

            Endereco objEndereco = new Endereco()
            {
                End = "Rua Rodolfo Garcia, 1988",
                Bairro = "Lagoa Nova",
                CEP = "59064370",
                Cidade = "Natal",
                UF = "RN"
            };
            #endregion

            #region criando o cedente, o agente que vai receber o dinheiro
            Cedente objCedente = new Cedente("08.286.940/0001-09", "PROCURADORIA GERAL DO ESTADO-RN", "3795", "8", "00005011", "3")
            {
                Codigo = cedente_convenio.ToString(),
                Carteira = "18",
                Convenio = cedente_convenio
            };
            #endregion

            #region criando um boleto que tem um sacado e especie de documento
            Boleto objBoleto = new Boleto(vencimento, valorBoleto, objCedente.Carteira, boleto_nossoNumero, objCedente)
            {
                ValorCobrado = valorBoleto,
                NumeroDocumento = numeroDocumento,
                LocalPagamento = "PAGÁVEL NA REDE BANCARIA ATÉ O VENCIMENTO",
                Sacado = new Sacado(sacado_cpfCnpj, sacado_nome, objEndereco),
                EspecieDocumento = new EspecieDocumento_BancoBrasil("23")
            };
            #endregion

            #region Adicionando as Instruções no Boleto 
            Instrucao_BancoBrasil instrucao = new Instrucao_BancoBrasil(1);
            instrucao.Descricao = "<small>";
            instrucao.Descricao += "Não conceder qualquer desconto ou abatimento";
            instrucao.Descricao += "<br>";
            instrucao.Descricao += "Não receber/pagar após o vencimento, pois o pagamento em atraso está sujeito ";
            instrucao.Descricao += "a juros de mora, multa de mora de 0,13% ao dia até o limite de 4% ";
            instrucao.Descricao += "e implicará em resíduo que impedirá a baixa do parcelamento.";
            instrucao.Descricao += "<br>";
            instrucao.Descricao += "Após o vencimento, comparecer à sede da PGE ou núcleos regionais ou ainda imprimir novo ";
            instrucao.Descricao += "boleto pelo site da PGE (www.pge.rn.gov.br).";
            instrucao.Descricao += "<br>";
            instrucao.Descricao += "Vlr. Total do Boleto R$ " + valorBoleto;
            instrucao.Descricao += "</small>";
            objBoleto.Instrucoes.Add(instrucao);
            #endregion

            return objBoleto;
        }                 

    }
}
