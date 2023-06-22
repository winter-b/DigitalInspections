<template>
    <div class="login-container">
      <h1>Prisijungimas</h1>
      <form @submit="login">
        <label for="username">Vartotojo vardas</label>
        <input type="text" id="username" v-model="formData.username" required>
        <br>
        <label for="password">Slaptažodis</label>
        <input type="password" id="password" v-model="formData.password" required>
        <br>
        <button type="submit" class="btn btn-primary">Prisijungti</button>
      </form>
      <p v-if="errorMessage" class="error-message">{{ errorMessage }}</p>
    </div>
  </template>
  <script>
  
  export default {
    data() {
      return {
        formData: {
          username: '',
          password: '',
        },
        errorMessage: '',
      };
    },
    methods: {
      async login(event) {
        event.preventDefault(); // Prevent form submission
        
        try {
            const form = new FormData();
            form.append("username", this.formData.username);
            form.append("password", this.formData.password);
            console.log()
            var response = fetch("http://backend.localhost/login", {
            method: "POST",
            headers: {
                "accept": "*/*"
            },
            body: form
            });
          console.log((await response).status)
          if ((await response).status === 200) {
            // get data to token from response body
            const token = (await response).text();
            localStorage.setItem('token', (await token));
            
            // Redirect to the home page or navigate using Vue Router
            this.$router.push('/');
            location.reload();
            //reload the page so the state of the token is updated
          } else if ((await response).status === 201) {
            // get data to token from response body
            const token = (await response).text();
            localStorage.setItem('token', (await token));
            
            // Redirect to the home page or navigate using Vue Router
            this.$router.push({ path: '/password/update', query: { redirected: true } });
            location.reload();
            //reload the page so the state of the token is updated
          } 
          else {
            this.errorMessage = 'Incorrect credentials. Please try again.';
          }
        } catch (error) {
          console.error('Error during login:', error);
          this.errorMessage = 'An error occurred during login. Please try again later.';
        }
      },
    },
  };
  
  </script>
  

  <style scoped>
  .login-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    height: 100vh;
  }
  
  .login-container h1 {
    margin-bottom: 20px;
  }
  
  .login-container form {
    display: flex;
    flex-direction: column;
    align-items: center;
  }
  
  .login-container label {
    margin-bottom: 10px;
  }
  
  .login-container input[type="text"],
  .login-container input[type="password"] {
    width: 250px;
    padding: 5px;
    margin-bottom: 10px;
  }
  
  .login-container button[type="submit"] {
    padding: 10px 20px;
  }
  
  .error-message {
    color: red;
    margin-top: 10px;
  }
  </style>