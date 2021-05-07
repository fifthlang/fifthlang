# The Fifth Programming Languge

![Twitter Follow](https://img.shields.io/twitter/follow/aabs?style=social)
[![.NET](https://github.com/aabs/fifthlang/actions/workflows/dotnet.yml/badge.svg)](https://github.com/aabs/fifthlang/actions/workflows/dotnet.yml)
![Travis (.com)](https://img.shields.io/travis/com/aabs/fifthlang)
![GitHub](https://img.shields.io/github/license/aabs/fifthlang)
![GitHub forks](https://img.shields.io/github/forks/aabs/fifthlang?style=social)
![GitHub Sponsors](https://img.shields.io/github/sponsors/aabs?style=social)
![GitHub language count](https://img.shields.io/github/languages/count/aabs/fifthlang)
![GitHub repo file count](https://img.shields.io/github/directory-file-count/aabs/fifthlang)


Fifth is (growing to be) a general purpose programming language running on the .NET tech stack that makes it easy to work with RDF knowledge Graphs.

## Table of Contents

- [Examples](#examples)
- [Contributing](#contributing)
- [License](#license)

## Examples

Here's an example of Fifth in action:

```csharp
class Person {
    Name: string;
    Height: float;
    Age: float;
    Weight: float;
}

void main(){
    Person p = new Person{
        Name = "Eric Morecombe",
        Height = 1.93,
        Age = 60,
        Weight = 95
    };

    p.Weight = p.Weight + 5;
    return p.Weight;
}
```

See [more examples](https://github.com/fifthlang/fifthlang/wiki/Example-Code#example-code), along with other docs, over on the wiki.

## Contributing

Feel free to dive in! [Open an issue](https://github.com/aabs/fifthlang/issues/new) or submit PRs.

Fifthlang follows the [Contributor Covenant](http://contributor-covenant.org/version/1/3/0/) Code of Conduct.

### Contributors

This project exists thanks to all the people who contribute. 
<a href="https://github.com/aabs/fifthlang/graphs/contributors"><img src="https://opencollective.com/fifthlang-contributors/contributors.svg?width=890&button=false" /></a>

## License

[GPL3.0 © Andrew Matthews.](../LICENSE)
