## Projeto para a disciplina "Linguagens de Programação 2"

---

## IMDB Analizer

Da Autoria de:

- Pedro Fernandes, nº 21803791
- Rafael Castro e Silva, nº 21903059
- João Moreira, nº 21801608

---

### Participação dos membros no projeto (como refletido pelos commits no Git):

- O trabalho começou a ser desenvolvido pelo grupo a ser consistido apenas pelo 
Pedro e o Rafael, este progresso está presente no _branch_ `master`. 

- Mais tarde o João juntou-se ao grupo e trouxe consigo o projecto num estado
muito mais completo e desenvolvido; o grupo adoptou então o código feito pelo 
João antes dele se juntar ao grupo. Este código está present no _branch_ `JoaoBranch`
e `JoaoBRanch`

#### Quanto á versão velha do projecto em `master`

- No primeiro commit feito pelo membro Rafael Castro e 
Silva - foi utilizada uma outra conta, "EldirishInquisition".

- O Pedro trabalhou no _backend_ do programa, ler e interpretar os ficheiros da
base de dados; Enquanto que o Rafael trabalhou no _search engine_, pegar na 
informação obtida pelo trabalho do Pedro (não chegou a ser implementado), 
e realizar as pesquisas nas coleções obtidas das bases de dados.

#### Quanto ás restantes versões do projeto incluindo a final

- Depois disto o Rafael e o Pedro trataram de comentar e fazer refactoring do codigo
para que este seja mais legivel e se aproxime mais dos principios _KISS_. O Rafael
tratou do refactoring maior e o Pedro de comentar e executar algumas alterações
de codigo para a qualidade do mesmo.No entanto a base de código tem na sua maioria 
a autoria do João.

- Fora do código, o Rafael e o Pedro escreveram este relatorio, 
o Pedro fez o _UML_.  

- No seu estado final para entrega, o projecto encontra-se no _branch_ `newMaster`.


---
### Arquitetura da Solução

- Foram usados, principalmente, dicionários para guardar a informação. Desta maneira é possivel realizar procuras rápidas com o "Where" usando apenas uma "Key" no método TryGetValue do dicionário.

- Usamos classes separadas para cada elemento da base de dados implementados por uma interface que os englobe a todos. Desta forma é possivel agregar as classes numa só interface.(ex : IEnumerable<IIMDBValue>...).

- Usamos várias interfaces para o código ser mais fléxivel. E também deixamos as classes em aberto i.e. as classes podem ser herditadas para outros programadores implementarem a sua própria solução usando as mesmas.

- Para os menus usamos métodos lambda para as ações do menus.

- Usamos o pattern Target para a resolução do problema.

#### UML

[Inserir aqui o UML]

---

### Referências:

- API .Net
- StackOverflow
