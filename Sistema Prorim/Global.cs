using System;      
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sistema_prorim
{
    class Global
    {   //----------------------------------------------------------------------------------------
        public static class Logon
        {
            private static string m_usuario;

            public static string usuario // variável para receber o nome do usuário que irá logar
            {
                get { return m_usuario; }
                set { m_usuario = value; }
            }

            private static string m_ipservidor;

            public static string ipservidor // variável para receber o ip do servidor
            {
                get { return m_ipservidor; }
                set { m_ipservidor = value; }
            }

            private static string m_nome_usuario;

            public static string nome_usuario // variável para receber o nome do usuário que irá logar
            {
                get { return m_nome_usuario; }
                set { m_nome_usuario = value; }
            }

            private static string m_cod_usuario;

            public static string codigo_usuario // variável para receber o nome do usuário que irá logar
            {
                get { return m_cod_usuario; }
                set { m_cod_usuario = value; }
            }            

            private static string m_senha = "";

            public static string senha
            {
                get { return m_senha; }
                set { m_senha = value; }
            }

            private static string m_tipoRequisicao = "";

            public static string tipoRequisicao
            {
                get { return m_tipoRequisicao; }
                set { m_tipoRequisicao = value; }
            }

            private static string m_tipousuario = "";

            public static string tipousuario // variável para receber o tipo do usuário M-Master ou C-Comum que irá logar
            {
                get { return m_tipousuario; }
                set { m_tipousuario = value; }
            } 

        }
        //----------------------------------------------------------------------------------------
        public static class DadosRim {

            private static string m_codigo = "";

            public static string codigo // variável que recebe valor sequencial de uma RI
            {
                get { return m_codigo; }
                set { m_codigo = value; }       
            }

            private static string m_cetil = "";

            public static string cetil // variável que recebe valor sequencial de uma RI
            {
                get { return m_cetil; }
                set { m_cetil = value; }
            }

            
            private static string m_escolhaforn = "";

            public static string escolhaforn // variável que recebe valor sequencial de uma RI
            {
                get { return m_escolhaforn; }
                set { m_escolhaforn = value; }
            }

            private static string m_escolhaUnid = "";

            public static string escolhaUnid // variável que recebe valor sequencial de uma RI
            {
                get { return m_escolhaUnid; }
                set { m_escolhaUnid = value; }
            }


            private static string m_descricao = "";

            public static string descricao // variável que recebe valor sequencial de uma RI
            {
                get { return m_descricao; }
                set { m_descricao = value; }
            }

            private static string m_DO = "";

            public static string DO // variável que recebe valor sequencial de uma RI
            {
                get { return m_DO; }
                set { m_DO = value; }
            }

            private static string m_dataCetil = "";

            public static string dataCetil // variável que recebe valor sequencial de uma RI
            {
                get { return m_dataCetil; }
                set { m_dataCetil = value; }
            }

            private static string m_valorEstimado = "";

            public static string valorEstimado // variável que recebe valor sequencial de uma RI
            {
                get { return m_valorEstimado; }
                set { m_valorEstimado = value; }
            }

            private static string m_valorReal = "";

            public static string valorReal // variável que recebe valor sequencial de uma RI
            {
                get { return m_valorReal; }
                set { m_valorReal = value; }
            }
                       
            private static string m_Processo = "";
            
            public static string Processo // variável que recebe valor sequencial de uma RI
            {
                get { return m_Processo; }
                set { m_Processo = value; }
            }

            private static string m_AnoProcesso = "";

            public static string AnoProcesso // variável que recebe valor sequencial de uma RI
            {
                get { return m_AnoProcesso; }
                set { m_AnoProcesso = value; }
            }

            private static string m_ProcessoContabil = "";

            public static string ProcessoContabil // variável que recebe valor sequencial de uma RI
            {
                get { return m_ProcessoContabil; }
                set { m_ProcessoContabil = value; }
            }

            private static string m_AnoProcessoContabil = "";

            public static string AnoProcessoContabil // variável que recebe valor sequencial de uma RI
            {
                get { return m_AnoProcessoContabil; }
                set { m_AnoProcessoContabil = value; }
            }

            private static string m_DataContabilidade = "";

            public static string DataContabilidade // variável que recebe valor sequencial de uma RI
            {
                get { return m_DataContabilidade; }
                set { m_DataContabilidade = value; }
            }
                      
            private static string m_DataOrdenador1 = "";

            public static string DataOrdenador1 // variável que recebe valor sequencial de uma RI
            {
                get { return m_DataOrdenador1; }
                set { m_DataOrdenador1 = value; }
            }

            private static string m_DataPrefeito = "";

            public static string DataPrefeito // variável que recebe valor sequencial de uma RI
            {
                get { return m_DataPrefeito; }
                set { m_DataPrefeito = value; }
            }

            private static string m_DataCompras1 = "";

            public static string DataCompras1 // variável que recebe valor sequencial de uma RI
            {
                get { return m_DataCompras1; }
                set { m_DataCompras1 = value; }
            }
            
            private static string m_DataOrdenador2 = "";

            public static string DataOrdenador2 // variável que recebe valor sequencial de uma RI
            {
                get { return m_DataOrdenador2; }
                set { m_DataOrdenador2 = value; }
            }


            private static string m_DataCompras2 = "";
             
            public static string DataCompras2 // variável que recebe valor sequencial de uma RI
            {
                get { return m_DataCompras2; }
                set { m_DataCompras2 = value; }
            }

            private static string m_DataDipe = "";

            public static string DataDipe // variável que recebe valor sequencial de uma RI
            {
                get { return m_DataDipe; }
                set { m_DataDipe = value; }
            }

            private static string m_cadastradoPor = "";

            public static string cadastradoPor // variável que recebe valor sequencial de uma RI
            {
                get { return m_cadastradoPor; }
                set { m_cadastradoPor = value; }
            }


            private static string m_dtCadastro = "";

            public static string dtCadastro // variável que recebe valor sequencial de uma RI
            {
                get { return m_dtCadastro; }
                set { m_dtCadastro = value; }
            }

            private static string m_Obs = "";

            public static string Obs // variável que recebe valor sequencial de uma RI
            {
                get { return m_Obs; }
                set { m_Obs = value; }
            }      
        }
        //----------------------------------------------------------------------------------------
        public static class NotaFiscal {


            private static string m_fornecedor="";

            public static string fornecedor // variável para receber o codigo do fornecedor
            {
                get { return m_fornecedor; }
                set { m_fornecedor = value; }
            }


            private static string m_codigoRI = "";

            public static string codigoRI // variável para receber o codigo da RI vinculada
            {
                get { return m_codigoRI; }
                set { m_codigoRI = value; }
            }


            private static string m_nomefornecedor = "";

            public static string nomefornecedor // variável para receber o codigo do fornecedor
            {
                get { return m_nomefornecedor; }
                set { m_nomefornecedor = value; }
            }
                
        }
        //----------------------------------------------------------------------------------------
        public static class despesa {

            private static string m_coddespesa = "";

            public static string coddespesas 
            {
                get { return m_coddespesa; }
                set { m_coddespesa = value; }
            }


            private static string m_despesa = "";

            public static string despesas
            {
                get { return m_despesa; }
                set { m_despesa = value; } 
            }

            private static string m_empenhoTotal = "";

            public static string empenhoTotal // variável para receber o tipo do usuário M-Master ou C-Comum que irá logar
            {
                get { return m_empenhoTotal; }
                set { m_empenhoTotal = value; }
            }


            private static string m_flag_valor_real = "";

            public static string flag_valor_real // variável que controla a alteração no valor real foi salvo na Requisição.
            {
                get { return m_flag_valor_real; }
                set { m_flag_valor_real = value; }
            }
                      
        }
        //----------------------------------------------------------------------------------------
        public static class RI {

            private static string m_cetil = "";

            public static string cetil
            {

                get { return m_cetil; }
                set { m_cetil = value; } 
            
            }


            private static string m_codcetil = "";

            public static string codcetil
            {

                get { return m_codcetil; }
                set { m_codcetil = value; }

            }
        }

        //----------------------------------------------------------------------------------------
        public static class fornecedor
        {
            private static string m_codfornecedor = "";

            public static string codfornecedor
            {
                get { return m_codfornecedor; }
                set { m_codfornecedor = value; }
            }

        }
        //----------------------------------------------------------------------------------------
        public static class Veiculos
        {
            private static string m_veiculo = "";

            public static string veiculo
            {

                get { return m_veiculo; }
                set { m_veiculo = value; }

            }
            
            private static string m_unidade = "";

            public static string unidade
            {

                get { return m_unidade; }
                set { m_unidade = value; }

            }

            private static string m_placa = "";

            public static string placa
            {

                get { return m_placa; }
                set { m_placa = value; }

            }
            private static string m_codPlaca = "";

            public static string codPlaca
            {

                get { return m_codPlaca; }
                set { m_codPlaca = value; }

            }

            private static string m_modelo = "";

            public static string modelo
            {

                get { return m_modelo; }
                set { m_modelo = value; }

            }

            private static string m_marca = "";

            public static string marca
            {

                get { return m_marca; }
                set { m_marca = value; }

            }

            private static string m_ano= "";

            public static string ano
            {

                get { return m_ano; }
                set { m_ano = value; }

            }

            private static string m_quant_placa = "";

            public static string quantPlaca
            {

                get { return m_quant_placa; }
                set { m_quant_placa = value; }

            }      
        }
        //----------------------------------------------------------------------------------------
        public static class InclusaoRI
        {
            private static int m_flagIncluirRim = 1;

            public static int flagIncluirRim
            {
                get { return m_flagIncluirRim; }
                set { m_flagIncluirRim = value; }
            }       
        }
        //----------------------------------------------------------------------------------------
    
    }
}
