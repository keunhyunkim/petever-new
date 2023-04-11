const express = require('express');
const multer = require('multer');
const bodyParser = require('body-parser');
const app = express();
const port = 3200;
const spawn = require('child_process').spawn;

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

const storage = multer.diskStorage({
	destination: function (req, file, cb) {
		cb(null, '/home/ubuntu/petever')
	},
	filename: function (req, file, cb) {
		cb(null, Date.now() + '-' + file.originalname)
	}
});

const upload = multer({ storage: storage });

function extractColor(filename, breedName, res) {
	
	// Extract Face Feature Color
	const pythonProcess = spawn('python', ['extract_color_xy.py', filename, breedName]);

	let outputData = '';

	pythonProcess.stdout.on('data', (data) => {
		outputData += data;
		console.log('Color script output:' + breedName + " " + data + '\n');
	});
	pythonProcess.stderr.on('data', (data) => {
		console.log('stderr:' + data);
	});
	pythonProcess.on('close', (code) => {
		console.log('child process exited with code ' + code);
		outputData = outputData.replace('(', '');
		outputData = outputData.replace(')', '');
		outputData = outputData.replace(/\'/g, '');

		let colors = outputData.split(',', 2);

		const breed = breedName;
		const status = 'success';
		const message = 'rcv img done';
		const content = {
				section1: colors[0],
				section2: colors[1],
			};
		console.log(colors[0], colors[1]);
		res.json({ breed, status, message, content });
	});
}

// upload.single(###) ### should be matched with Android interface
app.post('/image', upload.single('image'), (req, res) => {
	console.log('image received!!!');

	const {filename} = req.file;
	const imagePath = '/home/ubuntu/petever' + filename;
	let breed;

	// Classify breed
	const breedPython = spawn('python', ['breed_10_classification.py', filename]);

	breedPython.stdout.on('data', (data) => {
		breed += data;
		console.log('Breed script output:' + breed + '\n');
	});
	breedPython.stderr.on('data', (data) => {
		console.log('stderr:' + data);
	});

	breedPython.on('close', (code) => {
		extractColor(filename, breed, res);
	});

});

app.post('/abc', (req, res) => {
	console.log('abc received!!!');
	const status = 'success';
	const message = 'rcv abc done';
	res.json({ status, message });
});

app.listen(port, () => {
	console.log(`Server running on port ${port}`);
});
