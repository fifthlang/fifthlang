# Generics and IFifthType

> Q: If I want to add generics, how should I do it?

I presume that it would be better to add generics before any collections classes are added, so that the system is natively generic.

## Syntax Examples

```cpp
template <typename T>
class List {
  // Class contents.
};

template <typename T>
void Swap(T& a, T& b) {
  T temp = b;
  b = a;
  a = temp;
}
```

```csharp
class List<T> {
  // Class contents.
}

template <typename T>
void Swap<T>(T a, T b) {
  T temp = b;
  b = a;
  a = temp;
}
```

```haskell
data BinTree a = Leaf a | Node (BinTree a) a (BinTree a)
      deriving (Eq, Show)
```

```eiffel

class LIST [G] ...

item: G do ... end
put (x: G) do ... end
```

## Approaches to implementing Generics

The underlying platform .NET already has support for generics.  Perhaps there is a way to delegate the generics to that?
Generally, though, what are the ways in which a generic thing can be represented, formed, checked, and generated at runtime?

Languages like C++ initially worked from a rather simplistic macro expansion method, whereby the type parameters were injected into the template, prior to generation.  Assuming that that was the chosen option, that would present the following problems:

1. At compile time, how do you represent the structure of a thing that has type parameters rather than types in its specifications
2. At runtime, how do you form a concrete implementation of a generic thing, for dispatching?
