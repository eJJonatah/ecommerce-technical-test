# 3. Modelo de arquitetura escolhido

######  Date: 2026-09-24 4:42pm

## Contexto (Organização/estruturação de pastas e paradigmas a serem adotados)

Um modelo de arquitetura para arquivos/pastas precisa ser adotado para melhor estruturar o projeto \
Também sendo o principal criterio avaliativo como descrito pela documentação na seção do ASL

> Stack Obrigatória \
> • .NET 10 (Minimal API ou Controllers -justifique a escolha no README) \
> • Clean Architecture: camadas Domain, Application, Infrastructure, API \
> • CQRS com MediatR: commands e queries separados
>
> ~ docs\(ASL) Teste Prático...

## Decisão (Utilização do modelo de arquitetura hexagonal junto ao DDD e .NET Controllers)

Será desenvolvido utilizando os conceitos e modelos de pasta previstos na arquitetura hexagonal e \
DDD (domain-driven-design) ao invés de um simples mvc (que não exploraria conceitos descritos na \
documentação do projeto) e cito a [conferência de Mufrid Krilic](https://www.youtube.com/watch?v=Uwx1uCc5rxk&t=3
76 s) em Olso 2025 pela NDC como objeto \
de estudos da implementação deste conceito.

## Motivação (Formato descrito do projeto e considerações futuras)

A documentação-enunciado já cita diversos dos conceitos idealizados pela DDD e a arquitetura \
hexagonal ser um forte encorajador do uso das camadas idealizadas pela DDD, sendo um modelo robusto \
para mudanças e extensibilidade, que ocorrerá com certeza durante a defesa deste projeto

> ---> **Clean Architecture: camadas Domain, Application, Infrastructure, API**
>
> ~ docs\(ASL) Teste Prático...

## Consequências (Maior boilerplate em forma de arquivos e pastas )

Repetição de estruturas de código e maior aninhamento de pastas organizacionais sem valor lógico