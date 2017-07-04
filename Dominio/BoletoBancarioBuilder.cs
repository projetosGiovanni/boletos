using BoletoNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsBoletos.Dominio
{
    class BoletoBancarioBuilder
    {
        public short CodigoBanco { get; private set; }
        public bool MostrarCodigoCarteira { get; private set; }
        public Boleto Boleto { get; private set; }

        public BoletoBancarioBuilder ParaCodigoBanco(short codigoBanco)
        {
            this.CodigoBanco = codigoBanco;
            return this;
        }

        public BoletoBancarioBuilder ComBoleto(Boleto boleto)
        {
            this.Boleto = boleto;
            return this;
        }

        public BoletoBancarioBuilder ComMostrarCodigoCarteira(bool mostrarCodigoCarteira)
        {
            this.MostrarCodigoCarteira = mostrarCodigoCarteira;
            return this;
        }

        public void GerarBoletoPdf()
        {
            BoletoBancario objBoletoBancario = NewBoletoBancario();

            GerarPdf(objBoletoBancario);
        }

        public void GerarBoletoHtml()
        {
            BoletoBancario objBoletoBancario = NewBoletoBancario();

            GerarHtml(objBoletoBancario);
        }

        private BoletoBancario NewBoletoBancario()
        {
            BoletoBancario objBoletoBancario = new BoletoBancario()
            {
                CodigoBanco = this.CodigoBanco,
                Boleto = this.Boleto,
                MostrarCodigoCarteira = this.MostrarCodigoCarteira
            };
            //validar boleto
            objBoletoBancario.Boleto.Valida();
            return objBoletoBancario;
        }

        private void GerarPdf(BoletoBancario _bolBB)
        {
            //Crio array com os dados
            byte[] Pdf_Buffer = _bolBB.MontaBytesPDF(true);

            //Crio o arquivo em disco e um fluxo, sobrescrevendo o arquivo anterior, se existir.
            FileStream Stream = new FileStream(@"boleto.pdf", FileMode.Create);

            //Escrevo arquivo no fluxo
            Stream.Write(Pdf_Buffer, 0, Pdf_Buffer.Length);

            //Fecho fluxo pra finalmente salvar em disco
            Stream.Close();

            //Abrir PDF gerado
            System.Diagnostics.Process.Start(@"boleto.pdf");
        }

        private void GerarHtml(BoletoBancario _bolBB)
        {
            //crio o boleto como um arquivo *.html
            _bolBB.MontaHtmlNoArquivoLocal("boleto.html");

            //Abrir HTML no navegador padrão
            System.Diagnostics.Process.Start(@"boleto.html");
        }

    }
}
