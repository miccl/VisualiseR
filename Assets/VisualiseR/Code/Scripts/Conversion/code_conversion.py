import os

import sys

VALID_CODE_EXTENSIONS = [".java", ".cs", ".py", ".c"]
OUTPUT_FORMAT = 'jpeg'


def main():
    input_path, output_directory_path = get_input(sys.argv[1:])
    input_code_files = handle_input(input_path)
    for file_path in input_code_files:
        convert_code_file(file_path, output_directory_path)


def get_input(argv):
    try:
        input_path = argv[0]
        output_directory_path = argv[1]
        return input_path, output_directory_path
    except TypeError:
        print('usage: python code_conversion.py <input_path> <output_dir_path>')
        sys.exit(1)


def handle_input(input_path):
    if os.path.isdir(input_path):
        return traverse_directory(input_path)
    elif os.path.isfile(input_path):
        if is_code_file(input_path):
            return [input_path]
            # convert_code_file(input_path)
    else:
        print_error("The input path is not valid.")


def traverse_directory(dir_path):
    file_paths = []
    for file_name in os.listdir(dir_path):
        if is_code_file(file_name):
            file_path = '{0}{1}{2}'.format(dir_path, os.sep, file_name)
            file_paths.append(file_path)
    return file_paths


def is_code_file(file_name):
    return file_name.endswith(tuple(VALID_CODE_EXTENSIONS))


def convert_code_file(file_path, output_dir_path):
    output_file_name_with_extension = get_file_name_with_out_format(file_path, OUTPUT_FORMAT)
    output_file_path = '{0}{1}{2}'.format(output_dir_path, os.sep, output_file_name_with_extension)
    create_directory(output_dir_path)
    execute_pygment_on_file(OUTPUT_FORMAT, output_file_path, file_path)


def create_directory(output_dir_path):
    if not os.path.exists(output_dir_path):
        os.makedirs(output_dir_path)


def get_file_name_with_out_format(file_path, output_extension):
    file_name_with_extension = os.path.basename(file_path)
    file_name_without_extension = os.path.splitext(file_name_with_extension)[0]
    output_file_name_with_extension = '{0}{1}{2}'.format(file_name_without_extension, ".", output_extension)
    return output_file_name_with_extension


def execute_pygment_on_file(output_format, output_file, input_file):
    console_command = 'pygmentize -f {0} -o {1} {2}'.format(output_format, output_file, input_file)
    print(console_command)
    os.system(console_command)


def print_error(s):
    print("Error:", s)
    sys.exit(1)


if __name__ == "__main__":
    main()
