from flask import Flask, request
import openai

app = Flask(__name__)

# Set your OpenAI API key here (or load from an environment variable)
openai.api_key = "YOUR_OPENAI_API_KEY"

@app.route('/gpt', methods=['POST'])
def gpt():
    prompt = request.form.get('prompt', '')
    print("Received prompt:", prompt)
    try:
        response = openai.Completion.create(
            model="text-davinci-003",  # You can choose another model if you prefer
            prompt=prompt,
            max_tokens=50,
            temperature=0.7
        )
        # Extract and clean up the generated text
        answer = response.choices[0].text.strip()
    except Exception as e:
        answer = "Error: " + str(e)
    return answer

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000, debug=True)
