# 7. Usar classes ao invés de records nas entidades de domínio
######  Date: 2026-09-24 4pm

## Contexto (O C# 9+ possui os records como substitúto para classes rápidas)
É possível criar uma classe com diversas auto-implementações como .equals, .gethashcode e outros
sendo uma feature muito convidativa

## Decisão (Utilizar ambas ambordagens, uma para coleção de valores outra pra lógica)
Utilizar um record para agrupar os valores necessários para cada entidade e usar a classe para
implementar corretamente a validação de cada um dos campos de acordo com as indeias intrínsecas
de cada entidade. Separando a record (valores) da classEntity (validação)

## Motivação (Inclusão da validação no padrão comumn)
Utilizamos classes para que a declaração e implementação dos métodos de validação não estejam
em contato com sintaxe ainda experimental

## Consequências (Boilerplate e horas de desenvolimento)
Maior boilerplate e recurso do programador para declarar estas classes