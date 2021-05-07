# The Fifth Programming Languge

![Twitter Follow](https://img.shields.io/twitter/follow/aabs?style=social)
[![.NET](https://github.com/aabs/fifthlang/actions/workflows/dotnet.yml/badge.svg)](https://github.com/aabs/fifthlang/actions/workflows/dotnet.yml)
![Travis (.com)](https://img.shields.io/travis/com/aabs/fifthlang)
![GitHub](https://img.shields.io/github/license/aabs/fifthlang)
![GitHub forks](https://img.shields.io/github/forks/aabs/fifthlang?style=social)
![GitHub Sponsors](https://img.shields.io/github/sponsors/aabs?style=social)
![GitHub language count](https://img.shields.io/github/languages/count/aabs/fifthlang)
![GitHub repo file count](https://img.shields.io/github/directory-file-count/aabs/fifthlang)



Fifth is (to be) a general purpose programming language running on the .NET tech stack that makes it easy to work with RDF knowledge Graphs.

## Table of Contents

- [Examples](#security)
- [Background](#background)
- [Install](#install)
- [Usage](#usage)
- [API](#api)
- [Contributing](#contributing)
- [License](#license)

## Building the grammar from Powershell

To be able to run ANTLR, you must have a copy of java to run the Jar file:

```
sudo apt install openjdk-15-jre-headless
```

go to the root of the development hierarchy:

```
make grammar
```

Alternatively, to build within windows:

```powershell
java -jar .\tools\antlr-4.8-complete.jar -Dlanguage=CSharp -visitor -listener -o .\fifth.parser\grammar\ .\fifth.parser\grammar\Fifth.g4
```



## Examples

Here's an example of Fifth in action:

```csharp
class Person {
    Name: string;
    Height: float;
    Age: float;
    Weight: float;
}

float calculate_bmi(Person p{
    Age = age | age > 60,
    Height = height,
    Weight = weight
    }) {
    return weight / (height * height);
}

void main() {
    Person eric = new Person{
        Name = 'Eric Morecombe',
        Height = 1.93,
        Age = 65,
        Weight = 100
    };
    print(calculate_bmi(eric));
}
```

## Contributing

Feel free to dive in! [Open an issue](https://github.com/aabs/fifthlang/issues/new) or submit PRs.

Standard Readme follows the [Contributor Covenant](http://contributor-covenant.org/version/1/3/0/) Code of Conduct.

### Contributors

This project exists thanks to all the people who contribute. 
<a href="https://github.com/aabs/fifthlang/graphs/contributors"><img src="https://opencollective.com/standard-readme/contributors.svg?width=890&button=false" /></a>

## License

[GPL3.0 © Andrew Matthews.](../LICENSE)
