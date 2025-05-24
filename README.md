## Propostas Advocaticias

Gerador de **propostas de honorários advocatícios** escrito em .NET 9 que combina modelos DOCX preformatados, dados de clientes e serviços jurídicos para produzir documentos prontos para envio — tudo em poucos segundos e de forma totalmente containerizada.

### ✨ Principais recursos
- **Template-first**: usa um arquivo-modelo DOCX (`Assets/Model/ModelDocx.docx`) e realiza *placeholder replacement* via **DocumentFormat.OpenXml**, mantendo formatação, cabeçalhos e rodapés intactos.  
- **Catálogo de serviços**: carrega áreas do Direito e respectivos serviços a partir de JSON (`Assets/Engagement`) para montar propostas modulares.  
- **Cálculo automático de desconto**: aplica percentual à vista e exibe valor final formatado em `pt-BR`.  
- **CLI simples**: coleta dados do cliente no console e gera o arquivo em `Assets/Output/Proposta_<Cliente>_<timestamp>.docx`.  
- **Cross-platform & Dev-friendly**: código C# 13, `dotnet 9.0`, pronto para rodar em Windows, Linux e macOS.  
- **Docker support**: `Dockerfile` multi-stage (SDK → runtime) gera imagens enxutas; basta `docker run -it propostasadvocaticias`.

### 🚀 Como rodar

```bash
# 1. Clonar
git clone https://github.com/<usuario>/PropostasAdvocaticias.git
cd PropostasAdvocaticias

# 2. Executar localmente
dotnet run -c Release --project PropostasAdvocaticias

# 3. Via Docker
docker build -t propostasadvocaticias -f PropostasAdvocaticias/Dockerfile .
docker run --rm -it propostasadvocaticias
