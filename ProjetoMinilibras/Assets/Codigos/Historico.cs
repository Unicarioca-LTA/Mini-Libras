using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.IO;

public class Historico : MonoBehaviour
{
    // Referências de UI que você já possui linkadas no Inspector
    public Text textPercentFazenda, textPercentCores, textPercentCorpoHumano, txtPercentCidade,
                txtPercentCozinha, txtPercentBanheiro, txtPercentMercado, txtPercentParque,
                txtPercentEscola, txtPercentQuarto, txtPercentAlfabeto, txtPercentNumeros, txtPercentEmocoes;

    // Conexão com o Banco
    private string databaseName;
    private string databasePath;

    private SqliteConnection Connection => new SqliteConnection($"Data Source = {this.databasePath};");

    // Estruturas de Dados Dinâmicas para Otimização
    private class CategoryItem
    {
        public string goName;
        public string displayName;
        public CategoryItem(string go, string display) { goName = go; displayName = display; }
    }

    private class CategoryConfig
    {
        public string TableName;
        public string UICategoryName;
        public Text PercentText;
        public CategoryItem[] Items;

        public CategoryConfig(string table, string uiCat, Text pctText, CategoryItem[] items)
        {
            TableName = table; UICategoryName = uiCat; PercentText = pctText; Items = items;
        }
    }

    private List<CategoryConfig> categories;

    public void Desativar()
    {
        string[] pathsToDeactivate = {
            // Menina e Menino Personagens/Cabecas
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/personagem1",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/cabecaGeral/Cabeca1",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/personagem1",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/cabecaGeral/Cabeca1",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/personagem2",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/cabecaGeral/Cabeca2",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/personagem2",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/cabecaGeral/Cabeca2",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/personagem3",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/cabecaGeral/Cabeca3",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/personagem3",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/cabecaGeral/Cabeca3",
            // Cabelos e Franjas
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgCabelo/Cabelo1",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgFranja/Franja1",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgCabelo/Cabelo1",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgCabelo/Cabelo2",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgFranja/Franja2",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgCabelo/Cabelo2",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgCabelo/Cabelo3",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgFranja/Franja3",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/cabecaGeral/Cabelo3",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgFranja/Franja1",
            // Roupas
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgRoupaGeral/imgRoupa1",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgRoupaGeral/imgRoupa1",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgRoupaGeral/imgRoupa2",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgRoupaGeral/imgRoupa2",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgRoupaGeral/imgRoupa3",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgRoupaGeral/imgRoupa3",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/imgRoupaGeral/imgRoupa4",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/imgRoupaGeral/imgRoupa4",
            // Acessórios
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/AcessoriosImgs/imgAparelho1",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/AcessoriosImgs/imgAparelho1",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/AcessoriosImgs/imgAparelho2",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/AcessoriosImgs/imgAparelho2",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/AcessoriosImgs/imgAparelho3",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/AcessoriosImgs/imgAparelho3",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menina/AcessoriosImgs/imgOculos",
            "Canvas TelaInicial/Panel_LoadingPersonagem/SelecaoBoneco/Panel menino/AcessoriosImgs/imgOculos"
        };

        foreach (string path in pathsToDeactivate)
        {
            GameObject obj = GameObject.Find(path);
            if (obj != null) obj.SetActive(false);
        }
    }

    private void SetupCategories()
    {
        if (categories != null) return;
        categories = new List<CategoryConfig>();

        categories.Add(new CategoryConfig("fazenda", "Fazenda", textPercentFazenda, new CategoryItem[] {
            new CategoryItem("Sol", "Sol"), new CategoryItem("Nuvem", "Nuvem"), new CategoryItem("Cerca", "Cerca"),
            new CategoryItem("Casa", "Casa"), new CategoryItem("Galinha", "Galinha"), new CategoryItem("Pintinho", "Pintinho"),
            new CategoryItem("Pato", "Pato"), new CategoryItem("Porco", "Porco"), new CategoryItem("Vaca", "Vaca"),
            new CategoryItem("Ovelha", "Ovelha"), new CategoryItem("Cavalo", "Cavalo"), new CategoryItem("Trator", "Trator"),
            new CategoryItem("Lago", "Lago"), new CategoryItem("Horta", "Horta")
        }));

        categories.Add(new CategoryConfig("cores", "Cores", textPercentCores, new CategoryItem[] {
            new CategoryItem("Amarelo", "Amarelo"), new CategoryItem("Laranja", "Laranja"), new CategoryItem("Vermelho", "Vermelho"),
            new CategoryItem("Rosa", "Rosa"), new CategoryItem("Violeta", "Violeta"), new CategoryItem("Roxo", "Roxo"),
            new CategoryItem("Verde", "Verde"), new CategoryItem("Azul", "Azul"), new CategoryItem("Marrom", "Marrom"),
            new CategoryItem("Preto", "Preto"), new CategoryItem("Cinza", "Cinza"), new CategoryItem("Branco", "Branco"),
            new CategoryItem("Prateado", "Prateado"), new CategoryItem("Dourado", "Dourado"), new CategoryItem("Lilas", "Lilás")
        }));

        categories.Add(new CategoryConfig("CorpoHumano", "CorpoHumano", textPercentCorpoHumano, new CategoryItem[] {
            new CategoryItem("Cabeca", "Cabeça"), new CategoryItem("Nariz", "Nariz"), new CategoryItem("Ombro", "Ombro"),
            new CategoryItem("Cotovelo", "Cotovelo"), new CategoryItem("Mao", "Mão"), new CategoryItem("Joelho", "Joelho"),
            new CategoryItem("Orelha", "Orelha"), new CategoryItem("Olho", "Olho"), new CategoryItem("Boca", "Boca"),
            new CategoryItem("Barriga", "Barriga"), new CategoryItem("Perna", "Perna"), new CategoryItem("Pe", "Pé"),
            new CategoryItem("Unha", "Unha"), new CategoryItem("Cabelo", "Cabelo"), new CategoryItem("Dente", "Dente"),
            new CategoryItem("Lingua", "Língua"), new CategoryItem("Braco", "Braço")
        }));

        categories.Add(new CategoryConfig("cidade", "Cidade", txtPercentCidade, new CategoryItem[] {
            new CategoryItem("Predio", "Prédios"), new CategoryItem("Onibus", "Ônibus"), new CategoryItem("Ponto", "Ponto de ônibus"),
            new CategoryItem("Caminhao", "Caminhão"), new CategoryItem("Aviao", "Avião"), new CategoryItem("Carro", "Carro"),
            new CategoryItem("Faixa", "Faixa de pedestre"), new CategoryItem("Moto", "Moto"), new CategoryItem("Sinal", "Sinal de trânsito"),
            new CategoryItem("Poste", "Poste de luz"), new CategoryItem("Capacete", "Capacete"), new CategoryItem("Bicicleta", "Bicicleta"),
            new CategoryItem("Taxi", "Táxi"), new CategoryItem("Metro", "Metrô"), new CategoryItem("Trem", "Trem")
        }));

        categories.Add(new CategoryConfig("cozinha", "Cozinha", txtPercentCozinha, new CategoryItem[] {
            new CategoryItem("Agua", "Água"), new CategoryItem("Biscoito", "Biscoito"), new CategoryItem("Bolo", "Bolo"),
            new CategoryItem("Copo", "Copo"), new CategoryItem("Fogao", "Fogão"), new CategoryItem("Geladeira", "Geladeira"),
            new CategoryItem("Leite", "Leite"), new CategoryItem("Macarrao", "Macarrão"), new CategoryItem("Ovos", "Ovos"),
            new CategoryItem("Panela", "Panela"), new CategoryItem("Pao", "Pão"), new CategoryItem("Microondas", "Microondas"),
            new CategoryItem("Liquificador", "Liquificador")
        }));

        categories.Add(new CategoryConfig("banheiro", "Banheiro", txtPercentBanheiro, new CategoryItem[] {
            new CategoryItem("Cesto", "Cesto R. Suja"), new CategoryItem("Chuveiro", "Chuveiro"), new CategoryItem("Cortina", "Cortina"),
            new CategoryItem("Escova", "Escova de dentes"), new CategoryItem("Espelho", "Espelho"), new CategoryItem("Papel", "Papel higiênico"),
            new CategoryItem("Pasta", "Pasta de dente"), new CategoryItem("Pia", "Pia"), new CategoryItem("Sabonete", "Sabonete"),
            new CategoryItem("Shampoo", "Shampoo"), new CategoryItem("Toalha", "Toalha"), new CategoryItem("Vaso", "Vaso sanitário"),
            new CategoryItem("EscovaCabelo", "Escova de Cabelo"), new CategoryItem("Pente", "Pente")
        }));

        categories.Add(new CategoryConfig("mercado", "Mercado", txtPercentMercado, new CategoryItem[] {
            new CategoryItem("Abacaxi", "Abacaxi"), new CategoryItem("Banana", "Banana"), new CategoryItem("Batata", "Batata"),
            new CategoryItem("Laranja", "Laranja"), new CategoryItem("Maca", "Maça"), new CategoryItem("Melancia", "Melancia"),
            new CategoryItem("Morango", "Morango"), new CategoryItem("Pera", "Pera"), new CategoryItem("Tomate", "Tomate"),
            new CategoryItem("Uva", "Uva"), new CategoryItem("Carrinho", "Carrinho")
        }));

        categories.Add(new CategoryConfig("Parque", "Parque", txtPercentParque, new CategoryItem[] {
            new CategoryItem("Adulto", "Adulto"), new CategoryItem("Amarelinha", "Amarelinha"), new CategoryItem("Arvore", "Árvore"),
            new CategoryItem("Balanco", "Balanço"), new CategoryItem("Banco", "Banco"), new CategoryItem("Bola", "Bola"),
            new CategoryItem("Brincar", "Brincar"), new CategoryItem("Corda", "Corda de pular"), new CategoryItem("Crianca", "Criança"),
            new CategoryItem("Escorrega", "Escorrega"), new CategoryItem("Gangorra", "Gangorra"), new CategoryItem("Pulapula", "Pula-Pula")
        }));

        categories.Add(new CategoryConfig("escola", "Escola", txtPercentEscola, new CategoryItem[] {
            new CategoryItem("Brinquedo", "Brinquedo"), new CategoryItem("Giz", "Giz"), new CategoryItem("Globo", "Globo Terreste"),
            new CategoryItem("Lapis", "Lápis"), new CategoryItem("Livro", "Livro"), new CategoryItem("Mesa", "Mesa"),
            new CategoryItem("Mochila", "Mochila"), new CategoryItem("Oculos", "Óculos"), new CategoryItem("Papel", "Papel"),
            new CategoryItem("Professor", "Professora"), new CategoryItem("Quadro", "Quadro"), new CategoryItem("Relogio", "Relógio"),
            new CategoryItem("Cadeira", "Cadeira"), new CategoryItem("Caderno", "Caderno")
        }));

        categories.Add(new CategoryConfig("quarto", "Quarto", txtPercentQuarto, new CategoryItem[] {
            new CategoryItem("Abajur", "Abajur"), new CategoryItem("Armario", "Armário"), new CategoryItem("Cachorro", "Cachorro"),
            new CategoryItem("Cama", "Cama"), new CategoryItem("Computador", "Computador"), new CategoryItem("Gato", "Gato"),
            new CategoryItem("Janela", "Janela"), new CategoryItem("Lencol", "Lençol"), new CategoryItem("Porta", "Porta"),
            new CategoryItem("Travesseiro", "Travesseiro"), new CategoryItem("Tapete", "Tapete"), new CategoryItem("Pijama", "Pijama")
        }));

        // NOVAS TABELAS ADICIONADAS
        categories.Add(new CategoryConfig("alfabeto", "Alfabeto", txtPercentAlfabeto, new CategoryItem[] {
            new CategoryItem("A", "A"), new CategoryItem("B", "B"), new CategoryItem("C", "C"), new CategoryItem("D", "D"),
            new CategoryItem("E", "E"), new CategoryItem("F", "F"), new CategoryItem("G", "G"), new CategoryItem("H", "H"),
            new CategoryItem("I", "I"), new CategoryItem("J", "J"), new CategoryItem("K", "K"), new CategoryItem("L", "L"),
            new CategoryItem("M", "M"), new CategoryItem("N", "N"), new CategoryItem("O", "O"), new CategoryItem("P", "P"),
            new CategoryItem("Q", "Q"), new CategoryItem("R", "R"), new CategoryItem("S", "S"), new CategoryItem("T", "T"),
            new CategoryItem("U", "U"), new CategoryItem("V", "V"), new CategoryItem("W", "W"), new CategoryItem("X", "X"),
            new CategoryItem("Y", "Y"), new CategoryItem("Z", "Z")
        }));

        categories.Add(new CategoryConfig("numero", "Numeros", txtPercentNumeros, new CategoryItem[] {
            new CategoryItem("Um", "Um"), new CategoryItem("Dois", "Dois"), new CategoryItem("Tres", "Três"),
            new CategoryItem("Quatro", "Quatro"), new CategoryItem("Cinco", "Cinco"), new CategoryItem("Seis", "Seis"),
            new CategoryItem("Sete", "Sete"), new CategoryItem("Oito", "Oito"), new CategoryItem("Nove", "Nove"),
            new CategoryItem("Dez", "Dez"),  new CategoryItem("Zero", "Zero")
        }));

        categories.Add(new CategoryConfig("emocoes", "Emocoes", txtPercentEmocoes, new CategoryItem[] {
            new CategoryItem("Alegria", "Alegria"), new CategoryItem("Amor", "Amor"), new CategoryItem("Ansiedade", "Ansiedade"),
            new CategoryItem("Confusao", "Confusão"), new CategoryItem("Inveja", "Inveja"), new CategoryItem("Nojo", "Nojo"),
            new CategoryItem("Medo", "Medo"), new CategoryItem("Raiva", "Raiva"), new CategoryItem("Surpresa", "Surpresa"),
            new CategoryItem("Tedio", "Tédio"), new CategoryItem("Tristeza", "Tristeza"), new CategoryItem("Vergonha", "Vergonha")
        }));
    }

    public void porcentagensAcessos(Text nome)
    {
        this.databaseName = "dbLibras.db";
        this.databasePath = Path.Combine(Application.persistentDataPath, this.databaseName);

        // Preenche Titulo e Nome
        GameObject nomeTitulo = GameObject.Find("Canvas TelaInicial/Panel_Historico/ListaHistorico/Titulo");
        if (nomeTitulo != null) nomeTitulo.GetComponent<Text>().text = nome.text;

        GameObject nomeDetalhes = GameObject.Find("Canvas TelaInicial/Panel_Historico/Detalhes/Nome");
        if (nomeDetalhes != null) nomeDetalhes.GetComponent<Text>().text = nome.text;

        SetupCategories();

        foreach (var category in categories)
        {
            ProcessCategory(category);
        }
    }

    private void ProcessCategory(CategoryConfig config)
    {
        // 1. Desativar Barras Anteriores
        DisableAllBars(config.UICategoryName);

        // 2. Buscar Dados no Banco
        int[] itemsData = GetAcessos(config.TableName, config.Items.Length);

        // 3. Atualizar Porcentagem e Barra Ativa
        UpdatePercentageAndBar(itemsData, config.PercentText, config.UICategoryName);

        // 4. Atualizar os Textos de Detalhes
        UpdateDetails(config.UICategoryName, config.Items, itemsData);
    }

    private int[] GetAcessos(string tabela, int columnCount)
    {
        int[] results = new int[columnCount + 1]; // +1 pois os dados preenchem a partir do índice 1

        using (var dbConnection = Connection)
        {
            dbConnection.Open();
            using (var dbCmd = dbConnection.CreateCommand())
            {
                // Uso de parâmetro para evitar SQL Injection
                dbCmd.CommandText = $"SELECT * FROM {tabela} WHERE id = @id";
                dbCmd.Parameters.Add(new SqliteParameter("@id", CarregarPersonagem.personagemID));

                using (var reader = dbCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        for (int i = 1; i <= columnCount; i++)
                        {
                            if (!reader.IsDBNull(i))
                            {
                                results[i] = reader.GetInt32(i);
                            }
                        }
                    }
                }
            }
        }
        return results;
    }

    private void UpdatePercentageAndBar(int[] items, Text percentText, string categoryName)
    {
        if (percentText == null) return;

        int quantAcessados = 0;
        int maxItems = items.Length - 1; // Ignorando o índice 0 (ID)

        for (int i = 1; i <= maxItems; i++)
        {
            if (items[i] > 0) quantAcessados++;
        }

        int pctAcessos = maxItems > 0 ? (100 * quantAcessados) / maxItems : 0;
        percentText.text = pctAcessos.ToString() + "%";

        string barra = GetBarraName(pctAcessos);

        GameObject barraAtiva = GameObject.Find($"Canvas TelaInicial/Panel_Historico/ListaHistorico/Percent{categoryName}/{barra}");
        if (barraAtiva != null) barraAtiva.SetActive(true);
    }

    private void UpdateDetails(string categoryName, CategoryItem[] configItems, int[] dataItems)
    {
        for (int i = 0; i < configItems.Length; i++)
        {
            var item = configItems[i];
            int valor = dataItems[i + 1];

            GameObject valorObj = GameObject.Find($"Canvas TelaInicial/Panel_Historico/Detalhes/{categoryName}/{item.goName}");
            if (valorObj != null)
            {
                valorObj.GetComponent<Text>().text = $"{item.displayName}: {valor}";
            }
        }
    }

    private void DisableAllBars(string category)
    {
        string[] barNames = { "0-9", "10-19", "20-29", "30-39", "40-49", "50-59", "60-69", "70-85", "86-99", "100" };
        foreach (string bar in barNames)
        {
            GameObject b = GameObject.Find($"Canvas TelaInicial/Panel_Historico/ListaHistorico/Percent{category}/{bar}");
            if (b != null) b.SetActive(false);
        }
    }

    private string GetBarraName(int pct)
    {
        if (pct < 10) return "0-9";
        if (pct < 20) return "10-19";
        if (pct < 30) return "20-29";
        if (pct < 40) return "30-39";
        if (pct < 50) return "40-49";
        if (pct < 60) return "50-59";
        if (pct < 70) return "60-69";
        if (pct < 86) return "70-85";
        if (pct < 99) return "86-99";
        return "100";
    }
}