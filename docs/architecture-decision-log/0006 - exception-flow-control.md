# 1. Flow control com exceptions
######  Date: 2026-09-27 3:30pm

## Contexto (Flow control de falhas e errors)
Em cenários corporativos e ambientes de produção costuma-se ser contra indicado o uso de exceptions
para comunicação de erros, por possuirem um custo computacional elevado e quebrarem o fluxo natural
de código

## Decisão (Utilizar exceptions como flow control)
Apesar dos contrapontos supracitados ainda sim exceptions serão usadas como control flow,
notificação de problemas\falhas e validação de dados

## Motivação (Título)
.NET 10/C# não possuem uma implementação oficial de um objeto de resultado Result \<T, TErr\> e
apesar de sua implementação manual ou bibliotecas não serem muito complexas de adotar, por
motivs de tempo e simplicidade iremos recorrer ao método mais simples de lançar exceptions

## Consequências (Título)
Código mais lento e imprevisível, todavida cm impacto menor.