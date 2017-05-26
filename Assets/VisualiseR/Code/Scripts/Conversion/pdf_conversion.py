import os
import sys

VALID_PDF_EXTENSIONS = [".pdf"]
DENSITY = '300x300'
"""Output quality in percent"""
QUALITY = 100


def main():
    """
    Reads, handles and converts the input.
    """
    input_path, output_path = read_input(sys.argv[1:])
    check_input(input_path, output_path)
    convert_pdf_file(input_path, output_path)


def read_input(argv):
    """
    Reads the parameters of the command line.
    :param argv: arguments of the command line.
    """
    try:
        input_path = argv[0]
        output_path = argv[1]
        return input_path, output_path
    except TypeError:
        print('usage: python pdf_conversion.py <input_path> <output_path>')
        sys.exit(1)


def check_input(input_path, output_path):
    """
    Handles the input path and output path.
    Checks if the input path is pdf file and proceeds based on that.
    Creates necessary directories for output path.
    :param output_path: path of output image file.
    :param input_path: path of input pdf file.
    """
    if not (os.path.isfile(input_path)):
        if not (is_pdf_file(input_path)):
            print_error("The input path is not valid.")
            sys.exit(1)

    create_directory(os.path.dirname(output_path))


def is_pdf_file(file_name):
    """
    Whether or not the file is a pdf file.
    :param file_name: name of the file with extension.
    :return: True if the file is a pdf file, otherwise false
    """
    return file_name.endswith(tuple(VALID_PDF_EXTENSIONS))


def create_directory(dir_path):
    """
    Checks if a directory with the given path exists.
    If not, it creates it.
    :param dir_path: path of the directory
    """
    if not os.path.exists(dir_path):
        os.makedirs(dir_path)


def convert_pdf_file(file_path, output_path):
    """
    Converts pdf file.
    :param file_path: 
    :param output_path: 
    """
    execute_image_magick(file_path, output_path)


def execute_image_magick(input_file, output_file, density=DENSITY, quality=QUALITY):
    """
    Executes ImageMagick command  on command line.
    :param input_file: path of input pdf file.
    :param output_file:  path of output image file.
    :param density: density of the converted image.
    :param quality: quality of the converted image.
    """
    magick_command = 'magick convert '
    parameters = '-density {0} {1} -quality {2} {3}'.format(density, input_file, quality, output_file)
    os.system(magick_command + parameters)


def print_error(s):
    """
    Prints error message and terminates the application.
    :param s: string to print.
    """
    print("Error:", s)
    sys.exit(1)


if __name__ == "__main__":
    main()
