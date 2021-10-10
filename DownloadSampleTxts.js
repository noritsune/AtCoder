for (let i = 0; i < 3; i++) {
	const inSampleEle = document.getElementById(`pre-sample${i * 2}`);
	const outSampleEle = document.getElementById(`pre-sample${i * 2 + 1}`);
	if(inSampleEle == null) break;

	downloadTxtFile(`${i}In.txt`, inSampleEle.textContent);
	downloadTxtFile(`${i}Out.txt`, outSampleEle.textContent);
}

function downloadTxtFile(filename, textContent) {
	const blob = new Blob([textContent],{type:"text/plain"});
	const link = document.createElement('a');
	link.href = URL.createObjectURL(blob);
	link.download = filename;
	link.click();
}