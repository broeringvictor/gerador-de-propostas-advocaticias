# Propostas Advocaticias

.NET 9 · C# 13 · Docker · **Licença MIT**

Gerador automatizado de **propostas de honorários advocatícios**: combine um modelo DOCX, dados de cliente e serviços jurídicos definidos em JSON e receba um documento pronto para envio em poucos segundos.

---

## ✨ Principais Funcionalidades

| Funcionalidade           | Descrição                                                                                                                             |
| ------------------------ | ------------------------------------------------------------------------------------------------------------------------------------- |
| **Template Word**        | Substituição de *placeholders* no modelo DOCX (`Assets/Model/ModelDocx.docx`) via **DocumentFormat.OpenXml**, preservando formatação. |
| **Catálogo de serviços** | Áreas do Direito e serviços lidos de arquivos JSON em `Assets/Engagement`; basta criar ou editar seguindo o mesmo esquema.            |
| **Cálculo de valores**   | Aplica descontos percentuais à vista e formata valores em `pt‑BR`.                                                                    |
| **CLI simplificado**     | Coleta dados do cliente e gera `Proposta_<Cliente>_<timestamp>.docx` em `Assets/Output`.                                              |
| **Cross‑platform**       | Código .NET 9 roda em Windows, Linux e macOS, local ou em container.                                                                  |
| **Docker ready**         | *Dockerfile* multi‑stage gera imagem enxuta: `docker run -it propostasadvocaticias`.                                                  |

---

## 🏗️ Visão da Arquitetura

| Camada      | Stack                  | Responsabilidade                         |
| ----------- | ---------------------- | ---------------------------------------- |
| CLI         | .NET 9                 | Entrada de dados e orquestração do fluxo |
| Engine DOCX | DocumentFormat.OpenXml | Substituição de variáveis no modelo      |
| Dados       | JSON                   | Definição de áreas do Direito e serviços |
| Container   | Docker                 | Build multi‑stage e execução isolada     |

---

## 🚀 Primeiros Passos

### Pré‑requisitos

| Ferramenta        | Versão mínima |
| ----------------- | ------------- |
| .NET SDK          | 9.0.\*        |
| Docker (opcional) | 24+           |

### Instalação e execução

```bash
# Clonar o repositório
git clone https://github.com/<usuario>/PropostasAdvocaticias.git
cd PropostasAdvocaticias

# Rodar localmente
 dotnet run -c Release --project PropostasAdvocaticias

# Rodar em Docker
 docker build -t propostasadvocaticias -f PropostasAdvocaticias/Dockerfile .
 docker run --rm -it propostasadvocaticias
```

---

## 🔧 Decisões Técnicas

* **OpenXml**: manipula DOCX sem dependências Office/COM.
* **JSON**: permite adicionar novos serviços sem recompilar.
* **Docker multi‑stage**: builds reprodutíveis e portáveis.

---

## 📜 Licença

MIT — consulte o arquivo `LICENSE`.
