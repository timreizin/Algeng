# Algeng

Algeng is a simple interpreted programming language. To use the interpretator you need to have **.NET** installed.

The interpretator processes code line by line. You can write expression, statements, and define functions. Empty line denotes end of code, and the interpretator will finish it's work. Spaces are ignored. All errors in the code are **Undefined Behaviour**.

You can use +, -(binary as well as unary), * and (). +, -, * all have same precedence, so use () to evaluate something in different order. As operands you can have either numbers(only signed 64 bit integers), names of previously defined variables, or calls of previously defined functions. If you are outside of a function you can write an expression to get result of it’s evaluation outputted.

You can write **<variable\_name> = \<expression\>** to create a variables with that name that will contain the value the expression evaluates to. If a variable with the same name already exists its contents will be rewritten.

You can start function code by writing **<function\_name>[<expression\_1>, <expression\_2>, …, <expression\_n>]:**. The function cannot have 0 arguments. Functions consist of statements, which you write down just the same way as outside of function. The function always ends with an expression, evaluation of which will be returned.
