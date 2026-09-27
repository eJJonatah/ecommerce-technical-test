# 5. Exposição de classe-cliente para requisições da plataforma
###### Date: 2026-09-24 4pm

## Contexto (Exposição de features da api através de uma classe-cliente)
Exposição de uma classe pública no projeto

## Decisão (Expor uma classe-cliente importável pública)
Uma classe importável pública será disponibilizada na raíz do namespace com o objetivo de ser \
importada por outros assemblies consumidores, como por exemplo o assembly de teste

## Motivação (Para facilitar os testes e melhor descrever features esperadas)
Features primeiro serão descritas no cliente de requisição seguido de testes contra estas e \
por último a implementação das ações do código

## Consequências (Publicizar entidades e a implementação da classe-cliente)
Algumas classes de comunicação de dados (DTOs) e value-objects terão de ser publicizados para \
que os consumidores possam instanciar estes objetos