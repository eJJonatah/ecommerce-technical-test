# 1. Usar classes ao invés de records nas entidades de domínio
######  Date: 2026-09-24 4pm

## Contexto (O C# 9+ possui os records como substitúto para classes rápidas)
É possível criar uma classe com diversas auto-implementações como .equals, .gethashcode e outros
sendo uma feature muito convidativa

## Decisão (Ainda sim utilizar classes ao invés dos novos records)
Implementar manualmente quaisquer lógicas de ordenação, comparação ou outros comparadores assim
como redigir o boilerplate da declaração de campos e construtores


## Motivação (Inclusão da validação no padrão comumn)
Utilizamos classes para que a declaração e implementação dos métodos de validação não estejam
em contato com sintaxe ainda experimental

## Consequências (Boilerplate e horas de desenvolimento)
Maior boilerplate e recurso do programador para declarar estas classes