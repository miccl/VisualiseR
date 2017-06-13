# README

* _code_conversion.py_: Script for the conversion from code to jpg (including syntax highlithing and line numbering).
* _pdf_conversion.py_: Script for the conversion from pdf to jpg

## Code Conversion
The script allows to convert code files into JPEG-format.
Additionally it adds syntax highlighting and line numbering.
The script uses [Pygments](http://pygments.org) for the code conversion.

###Setup

In order to use the script you need the following:

* [Python 3.x](https://www.python.org/downloads/)
* [Pygments](http://pygments.org/download/)

### Usage
The script can used with the following command in the console:

```
python code_conversion.py <input_path> <output_dir_path>
```

**Parameters:**

* _input_path_: Path of the file or directory that should be converted.
* _output_dir_path_: Path where the converted files should be stored.

**Examples:**
Convert all files in directory.
```
python code_conversion.py C:/CodeDirectory C:/TargetDirectory/Test
```

Convert a file.
```
python code_conversion.py C:/CodeDirectory/test.py C:/TargetDirectory/Test
```

## Pdf Conversion
The script allows to convert pdf files into image format.
A new image file is created for every page of the pdf file.
The script uses [ImageMagick](https://www.imagemagick.org/script/index.php), which uses [Ghostscript](https://www.ghostscript.com/) for the conversion.

### Setup
In order to use the script you need the following:

* [Python 3.x](https://www.python.org/downloads/)
* [ImageMagick](https://www.imagemagick.org/script/download.php)
* [Ghostscript](https://www.ghostscript.com/download/)

### Usage
The script can be used with the following command in the console:

```
python pdf_conversion.py <input_path> <output_path>
```

**Parameters:**

* _input_path_: File of the pdf file that should be converted.
* _ouput_path_: Path of the image file where the converted files should be stored. 

**Examples:**

Convert all the pdf file in directory. 
```
python pdf_conversion.py C:/Path/To/Pdf/File/test.pdf D:/path/to/jpg/file/test.jpg
```
If the pdf has more than 1 page, it creates image files named test-0.jpg, test-1.jpg and so on.