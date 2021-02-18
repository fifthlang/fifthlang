# The Fifth Programming Languge

![Travis (.com)](https://img.shields.io/travis/com/aabs/fifthlang)
![GitHub](https://img.shields.io/github/license/aabs/fifthlang)

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
use std, io, lob, env, string ;

alias <http://xmlns.com/foaf/0.1/> as foaf;
alias <http://www.w3.org/1999/02/22-rdf-syntax-ns#> as rdf;
alias <http://www.lob.org/users> as loborg;

// get user token using IRI in environment varible
user_id = io.get_user_token(env.get_var("LOB_USER_ID"));

// set the base RDF fact store to derive from the facts in the user graph at lob.com
// needs to be defined on entry, because success of main needs to translate into a
// commit of new facts into the ground state store
with ground_state = lob.store(loborg, user_id);

main() {
    new_address = get_new_address();
    if (!is_valid_address(new_address)) {
        exit("invalid address");
    };
    only_home? = get_is_only_home();
    if (only_home? ) {
        self.address.until = std.today();
    };
    new_address.from = std.today();
    self.address = new_address;
}

get_new_address(){
    a1 = io.ask("address 1");
    a2 = io.ask("address 2");
    t = io.ask("town");
    p = io.ask("postcode");
    r = io.ask("region");
    c = io.ask("country");
    return new lob.Address{
        address1 = a1,
        address2 = a2,
        town = t,
        postcode = p,
        region = r,
        country = c
    };
}

is_valid_address(lob.Address a){
    return string.non_empty(a.address1) && 
        string.non_empty(a.town) && 
        string.non_empty(a.region) && 
        string.non_empty(a.postcode) && 
        string.non_empty(a.country);
}

get_is_only_home(){
    x = io.ask("will this be your only home?");
    return std.to_boolean(x);
}
```

### Any optional sections

## Background

### Any optional sections

## Install

This module depends upon a knowledge of [Markdown]().

```
```

### Any optional sections

## Usage

```
```

Note: The `license` badge image link at the top of this file should be updated with the correct `:user` and `:repo`.

### Any optional sections

## API

### Any optional sections

## More optional sections

## Contributing

See [the contributing file](CONTRIBUTING.md)!

PRs accepted.

Small note: If editing the Readme, please conform to the [standard-readme](https://github.com/RichardLitt/standard-readme) specification.

### Any optional sections

## License

[MIT © Richard McRichface.](../LICENSE)
