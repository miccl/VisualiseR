# README #

* code_conversion.py: Script for the conversion from code to jpg including syntax highlithing and line numbering.

## Code Conversion ##
The script allows to convert code files into JPEG-format.
Additionally it adds syntax highlighting and line numbering.
The script uses [Pygments](http://pygments.org) for the code conversion.

###Setup ###

In order to use the syntax_highlighter.py script you need the following stuff:

* [Python 3.x](https://www.python.org/downloads/)
* [Pygments](http://pygments.org/download/).

### Usage ###
The script can used with the following command in the console:

```
python code_conversion.py <input_path> <output_dir_path>
```

**Parameters:**

* input_path: Path of the file or directory that should be converted.
* output_dir_path: Path where the converted files should be stored.

**Examples:**
Convert all files in directory.
```
python code_conversion.py C:/CodeDirectory D:/TargetDirectory/Test
```

Convert a file.
```
python code_conversion.py C:/CodeDirectory/test.py D:/TargetDirectory/Test
```