# Episode 29: Local Language Model Roundup #1

Can a local LLM running on consumer hardware replace cloud AI for real coding tasks? In this episode, we find out. We pit **5 open-source models** against each other to see how they stack up — all running locally via [Ollama](https://ollama.com) on an AMD Ryzen 9 9990X 12-core processor with 96 GB RAM and an NVIDIA GeForce RTX 5090 graphics card with 32GB of VRAM. No cloud. No API keys. Just raw local inference.

📺 YouTube Video: https://youtu.be/MROj19EZ7I0

🏠 Code it with AI Home Page: https://codeitwithai.com

---

## The Test

The test was to host the model in Ollama on another machine and use GitHub Copilot CLI against it to flesh out a new Blazor Server app with a file manager page.

The **Gemma4Test** project is the result.

## Ollama Models and Results

### **Gemma4** 

Here's the prompt:

> I want to create an API-based file upload functionality. So, I need a Files folder to receive files. In the Blazor code I want to break a selected file up into chunks. You'll need a class (FileChunk.cs) that will be passed to the api endpoint. This class will have to keep the file name, the number of chunks, the chunk number of this chunk, a byte array to hold the data for the chunk. The api will open the file, seek to the appropriate location, and write the data. The Blazor page should show a progress bar as the file chunks are uploaded.

It created the API controller, the endpoint, and the FileChunk.cs class, but could not figure out how to write the Blazor page. 

### **Gpt-oss:latest**

Did not work at all.

### **Granite4.1:3b**

Couldn't tell me ANYTHING about the project. Hallucinated functionality that was not there.

### **laguna-xs.2**

Gave me a great description of the app and what it does.
Here's the prompt I gave it:

> I want to flesh out the FileManager page with a list of files and the ability to download, rename, and delete files. The list should update whenever a file is added, renamed, or deleted.

Verdict: Slow and not particularly thorough. I gave up on it

### **Qwen3.6:latest.**

Here's the prompt:

> In FileManager.razor I want to break a selected file up into chunks which will be passed to the api endpoint. The api will open the file, seek to the appropriate location, and write the data. The Blazor page should show a progress bar as the file chunks are uploaded.

Awesome. In the same class as qwen3-coder:30b. It found bugs as it was writing the code and fixed them. I would use this in the real world.

Once it had completed the file manager page, I gave it this prompt to fix bugs, which it did:

> The FileManager page doesn't show existing files correctly. First of all, I want to remove the uploads folder under the wwwroot. The files are stored in the Files folder. I want a method that gets all the files in the Files folder, uses a FileInfo to get the information, and shows it in the list on the FileManager page. This method needs be called on startup, and whenever a file is uploaded, renamed, or deleted. Currently it shows no file name and zero for the size.

### **qwen3-coder:30b**

Shown in episode 27. Pretty darn good. Couldn't implement chunked file uploads correctly in a previous test. I didn't use it for this project, which I started from scratch.

---

## Resource Links

- **Ollama:** https://ollama.com — Local model runtime
- **Ollama Model Library:** https://ollama.com/search — Browse available models
- **GitHub Copilot BYOK Docs:** https://docs.github.com/copilot/managing-copilot/managing-github-copilot-in-your-organization/managing-the-copilot-subscription-for-your-organization/managing-copilot-knowledge-bases
- **GitHub Copilot Official Docs:** https://docs.github.com/copilot

---

## License

This episode's content and code are part of the **Code it with AI** series.
Licensed under [MIT License](LICENSE).
