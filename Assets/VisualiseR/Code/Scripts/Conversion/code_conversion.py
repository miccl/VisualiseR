import os

import sys

VALID_CODE_EXTENSIONS = [".java", ".cs", ".py", ".c"]
OUTPUT_FORMAT = 'jpeg'


def main():
    """
    Reads, handles and converts the input.
    """
    input_path, output_directory_path = read_input(sys.argv[1:])
    input_code_files = handle_input(input_path)
    for file_path in input_code_files:
        convert_code_file(file_path, output_directory_path)


def read_input(argv):
    """
    Reads the parameters of the command line.
    :param argv: arguments of the command line.
    """
    try:
        input_path = argv[0]
        output_directory_path = argv[1]
        return input_path, output_directory_path
    except IndexError:
        print('Usage: python code_conversion.py <input_path> <output_dir_path>')
        sys.exit(1)


def handle_input(input_path):
    """
    Handles the input path.
    Checks if the input path is directory or code file and proceeds based on that.
    :param input_path: path of input.
    :return: list of code file paths.
    """
    if os.path.isdir(input_path):
        return traverse_directory(input_path)
    elif os.path.isfile(input_path):
        if is_code_file(input_path):
            return [input_path]
    else:
        print("Error: The input path '%s'is not valid." % input_path)
        sys.exit(1)


def traverse_directory(dir_path):
    """
    Returns all paths of code file in the directory.
    :param dir_path: path of the directory.
    :return: list of code file paths in the directory.
    """
    file_paths = []
    for file_name in os.listdir(dir_path):
        if is_code_file(file_name):
            file_path = '{0}{1}{2}'.format(dir_path, os.sep, file_name)
            file_paths.append(file_path)
    return file_paths


def is_code_file(file_name):
    """
    Whether or not the file is a code file.
    :param file_name: name of the file with extension.
    :return: True if the file is a code file, otherwise false
    """
    return file_name.endswith(tuple(VALID_CODE_EXTENSIONS))


def convert_code_file(file_path, output_dir_path):
    """
    Converts the file with the given file path and saves it in the given directory.
    Uses pygments.
    :param file_path: path of the file to convert
    :param output_dir_path: desired save directory
    """
    output_file_name_with_extension = get_file_name_with_out_format(file_path, OUTPUT_FORMAT)
    output_file_path = '{0}{1}{2}'.format(output_dir_path, os.sep, output_file_name_with_extension)
    create_directory(output_dir_path)
    execute_pygment_on_file(OUTPUT_FORMAT, output_file_path, file_path)


def create_directory(dir_path):
    """
    Checks if a directory with the given path exists.
    If not, it creates it.
    :param dir_path: path of the directory
    """
    if not os.path.exists(dir_path):
        os.makedirs(dir_path)


def get_file_name_with_out_format(file_path, output_extension):
    """
    Returns file path with desired extension.
    :param file_path:
    :param output_extension:
    :return:
    """
    file_name_with_extension = os.path.basename(file_path)
    file_name_without_extension = os.path.splitext(file_name_with_extension)[0]
    output_file_name_with_extension = '{0}{1}{2}'.format(file_name_without_extension, ".", output_extension)
    return output_file_name_with_extension


def execute_pygment_on_file(output_format, output_file, input_file):
    """
    Executes pygments bash command.
    :param output_format: conversion format (e.g. jpeg)
    :param output_file: path of the output file
    :param input_file: path of the input file
    """
    console_command = 'pygmentize -f {0} -o {1} {2}'.format(output_format, output_file, input_file)
    os.system(console_command)

if __name__ == "__main__":
    main()
