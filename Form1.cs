using System;
using System.IO;
using System.Windows.Forms;
using BoletoNet;

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
            //crio o boleto como um arquivo *.html
            RetornaBoletoPreenchido().MontaHtmlNoArquivoLocal("boleto.html");

            //Abrir HTML no navegador padrão
            System.Diagnostics.Process.Start(@"boleto.html");
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            //Crio array com os dados
            byte[] Pdf_Buffer = RetornaBoletoPreenchido().MontaBytesPDF(true);

            //Crio o arquivo em disco e um fluxo, sobrescrevendo o arquivo anterior, se existir.
            FileStream Stream = new FileStream(@"boleto.pdf", FileMode.Create);

            //Escrevo arquivo no fluxo
            Stream.Write(Pdf_Buffer, 0, Pdf_Buffer.Length);

            //Fecho fluxo pra finalmente salvar em disco
            Stream.Close();

            //Abrir PDF gerado
            System.Diagnostics.Process.Start(@"boleto.pdf");
        }

        private BoletoBancario RetornaBoletoPreenchido() {

            DateTime vencimento = Convert.ToDateTime("04/07/2017"); //txtVencimento.Text; 
            decimal valorBoleto = Convert.ToDecimal("1,59"); // txtValor.Text; 
            string numeroDocumento = "501953"; //"00000174"; //"10028528";  //"498261";
            int cedente_convenio = 2815189; //1220950;
            //string boleto_nossoNumero = "0" + cedente_convenio; //000[1002852] [2815189] [0001002852] nosso numero valido

            #region informações pessoais do sacado / pagador         
            string sacado_cpfCnpj = "034.648.884-28";
            string sacado_nome = "GIOVANNI DE PAIVA NICOLETTI"; //txtSacado.Text;

            Endereco objEndereco = new Endereco();
            objEndereco.End = "Rua Rodolfo Garcia, 1988";
            objEndereco.Bairro = "Lagoa Nova";
            objEndereco.CEP = "59064370";
            objEndereco.Cidade = "Natal";
            objEndereco.UF = "RN";
            #endregion

            #region objCedente - criando o objeto cedente, o agente que vai receber o dinheiro
            Cedente objCedente = new Cedente("08.286.940/0001-09", "PROCURADORIA GERAL DO ESTADO-RN", "3795", "8", "00005011", "3");
            objCedente.Codigo = cedente_convenio.ToString();
            objCedente.Carteira = "18";
            objCedente.Convenio = cedente_convenio;            
            #endregion

            #region objBoleto - criando um boleto que tem um sacado e especie de documento
            Boleto objBoleto = new Boleto(vencimento, valorBoleto, objCedente.Carteira, numeroDocumento, objCedente);
            objBoleto.NumeroDocumento = numeroDocumento;
            objBoleto.LocalPagamento = "PAGÁVEL NA REDE BANCARIA ATÉ O VENCIMENTO";
            objBoleto.Sacado = new Sacado(sacado_cpfCnpj, sacado_nome, objEndereco);
            objBoleto.EspecieDocumento = new EspecieDocumento_BancoBrasil("23");
            #endregion

            #region objBoleto - Adicionando uma Instrucao_BancoBrasil texto decritivo em HTML 
            Instrucao_BancoBrasil instrucao = new Instrucao_BancoBrasil(1);
            instrucao.Descricao += "<small>";
            instrucao.Descricao = "Não conceder qualquer desconto ou abatimento";
            instrucao.Descricao += "<br>";
            instrucao.Descricao += "Não receber/pagar após o vencimento, pois o pagamento em atraso está sujeito ";
            instrucao.Descricao += "a juros de mora, multa de mora de 0,13% ao dia até o limite de 4% ";
            instrucao.Descricao += "e implicará em resíduo que impedirá a baixa do parcelamento.";
            instrucao.Descricao += "<br>";
            instrucao.Descricao += "Após o vencimento, comparecer à sede da PGE ou núcleos regionais ou ainda imprimir novo ";
            instrucao.Descricao += "boleto pelo site da PGE (www.pge.rn.gov.br).";
            instrucao.Descricao += "<br>";
            instrucao.Descricao += "Vlr. Devido R$ x,xx + Vlr. Honorários R$ 0,00 = R$ " + valorBoleto;
            instrucao.Descricao += "</small>";
            objBoleto.Instrucoes.Add(instrucao);
            #endregion

            #region objBoletoBancario - cria o boleto a ser impresso
            BoletoBancario objBoletoBancario = new BoletoBancario();
            objBoletoBancario.CodigoBanco = 1;
            objBoletoBancario.Boleto = objBoleto;
            //objBoletoBancario.Boleto.Carteira = objCedente.Carteira;
            //objBoletoBancario.Boleto.LocalPagamento = "PAGÁVEL NA REDE BANCARIA ATÉ O VENCIMENTO";
            objBoletoBancario.Boleto.ValorCobrado = valorBoleto;
            objBoletoBancario.MostrarCodigoCarteira = true;
            objBoletoBancario.Boleto.Valida();
            #endregion

            return objBoletoBancario;
        }
    }
}
