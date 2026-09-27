# 4. Abordagem de desenvolvimento a partir dos testes (TDD)

######  Date: 2026-09-24 5:06pm

## Contexto (Qual o ponto de partida do desenvolvimento?)

Diversos projetos escolhem primeiro estruturação de pastas e posteriormente desenvolvimento de \
testes e provas de conceito, o que pode resultar em features desnecessárias ou testes insuficientes \
com isso em mente é necessário decidir à partir de qual ponto dar início ao desenvolvimento.

## Decisão (Escrever primeiro os testes a serem batidos, depois implementar lógica)

O primeiro projeto a ser criado será um xUnit test, como descrito no enunciado, onde os testes
serão criados e a partir disso o desenvolvimento deve chegar a este resultado previsto

## Motivação (A documentação-enunciado tráz diversas preocupações com testes)

É citado como conceito eliminatório o uso, validação e defesa de testes, como descrito em

> [...] Codigo limpo, bem estruturado e testavel vale mais do que muitos endpoints mal implementados.\
> \
> [...] \
> \
> [...] Não entregue sem pelo menos os testes unitários dos handlers \
> ~ docs\(ASL) Teste Prático...

## Consequências (Pragmatismo no desenvolvimento)

Algumas features consideradas não essenciais por não estarem descritas nos testes serão ignoradas para \
atingir o cenário especulado mais rapidamente