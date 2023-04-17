import os
from flask import Flask, request, jsonify
from werkzeug.utils import secure_filename
from importlib import util
import feature_extraction_tflite

app = Flask(__name__)
port = 3200

@app.route('/image', methods=['POST'])
def upload_image():
    file = request.files['image']
    filename = secure_filename(file.filename)
    file.save(os.path.join('/home/ubuntu/petever', filename))
    image_path = '/home/ubuntu/petever/' + filename
    output_data = ''
 
    # Import the Python script as a module
    output_data = feature_extraction_tflite.breed_main(image_path)

    if output_data is not None:
        output_data = ','.join(output_data)
        output_data = output_data.split(',')

        breed = output_data[2]
        status = 'success'
        message = 'img'
        content = {
            'section1': output_data[0],
            'section2': output_data[1]
        }
    else:
        breed = None
        status = 'error'
        message = 'Failed to process image'
        content = None
    
    #remove the file
    os.remove(image_path)

    return jsonify({'breed': breed, 'status': status, 'message': message, 'content': content})


if __name__ == '__main__':
    app.run(host='0.0.0.0', port=port)