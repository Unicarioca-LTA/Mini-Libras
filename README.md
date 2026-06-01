# MINI LIBRAS

## Sumário
- [Visão Geral](#visão-geral)
- [Informações Técnicas](#informações-técnicas)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Relatório Semanal](#relatório-semanal)

---

## Visão Geral

O **MINI LIBRAS** é um projeto desenvolvido no **Laboratório de Tecnologia Aplicada (LTA) - Unicarioca**, com o objetivo de **difundir o conhecimento e aprendizado de LIBRAS(lingua brasileira de sinais)**.

---

## Informações Técnicas

- **Versão do Unity:** Unity 6   
- **Plataforma:** Mobile    

> Use **exatamente a versão do Unity indicada acima**.  
> Abrir o projeto em outra versão pode causar falhas, erros de script ou arquivos corrompidos.

---

## Estrutura do Projeto

O projeto segue a organização oficial do LTA, baseada na separação entre conteúdo interno e pacotes externos (Plugins).  
Todo o conteúdo produzido pela equipe deve ficar dentro da pasta **`Assets/Game/`**.

Essa separação é importante porque, quando colocamos arquivos e pastas como `Scenes`, `Scripts`, `Prefabs` e todos os nossos assets dentro da pasta raiz (`Assets`) e baixamos plugins da Unity Store que criam arquivos semelhantes com muitas pastas, a raiz fica cheia de arquivos e difícil de navegar. Organizar dessa forma mantém o projeto limpo e facilita a manutenção futura.

> **Atenção:** apenas modifique arquivos dentro de `Assets/Game/`.  
> Pastas criadas automaticamente por plugins ou pelo Unity não devem ser movidas ou editadas, pois isso pode quebrar o projeto.
> A estrutura Assets/Game é importante manter, mas de resto vai existir situações que demandam novas pastas ou diferentes de acordo com cada projeto. nesses casos sigam conforme o necessário.


```bash
Assets/
└── Game/
    ├── Scenes/             # Cenas do jogo
    ├── Scripts/            # Códigos C#
    ├── Prefabs/            # Objetos reutilizáveis
    ├── Art/
    │   ├── Models/         # Modelos 3D
    │   ├── Materials/      # Materiais e shaders
    │   ├── Textures/       # Texturas PBR, Normal Maps etc.
    │   ├── UI/             # Ícones e imagens de interface
    │   └── Fonts/          # Fontes utilizadas na UI
    ├── Audio/              # Músicas e efeitos sonoros
    ├── Animations/         # Controladores e clips de animação
    └── Resources/          # Assets carregados em tempo de execução

```

---

## Relatório Semanal

Corrigido ao clicar no ícone "boca" uma vez, não ter como clicar nele de novo.

Corrigido ao abrir o painel da cor roxa, não ter como fechar.

Corrigido ao clicar no exemplo de uso da cor amarela, o texto sobrepor o desenho.

Corrigido ao abrir o painel da Batata, não ter como fechar.

Corrigido ao abrir o painel do Computador, não ter como fechar.

---

