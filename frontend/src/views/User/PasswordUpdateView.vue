<template>
    <div class="password-update-view">
      <h2>Slaptažodžio atnaujinimas</h2>
      <div v-if="showWarning" class="alert alert-danger">
      Jūsų slaptažodis senesnis nei 6 mėnesiai. Atnaujinkite slaptažodį.
      </div>      
      <div class="password-guidelines">
        <h3>Slaptažodžio gairės:</h3>
        <ul>
          <li>Slaptažodis turi būti ne trumpesnis kaip 7 ženklų.</li>
          <li>Slaptažodį turi sudaryti bent vienas skaičius, vienas simbolis ir po vieną mažąją ir didžiąją raidę.</li>
          <li>Venkite naudoti įprastus slaptažodžius arba asmeninę informaciją.</li>
        </ul>
      </div>
      <form @submit.prevent="updatePassword">
        <div>
          <label for="currentPassword">Dabartinis slaptažodis</label>
          <input type="password" id="currentPassword" v-model="currentPassword" required>
        </div>
        <div>
          <label for="newPassword">Naujas slaptažodis:</label>
          <input type="password" id="newPassword" v-model="newPassword" required>
        </div>
        <div>
          <label for="confirmPassword">Patvirtinti naują slaptažodį:</label>
          <input type="password" id="confirmPassword" v-model="confirmPassword" required>
        </div>
        <div>
          <button type="submit" class="btn btn-success">Atnaujinti slaptažodį</button>
        </div>
      </form>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        showWarning: false,
        currentPassword: '',
        newPassword: '',
        confirmPassword: '',
      };
    },
    mounted() {
    this.checkRedirectedFromLogin();
    },
    methods: {
        checkRedirectedFromLogin() {
      const redirected = this.$route.query.redirected;
      if (redirected && redirected === 'true') {
        this.showWarning = true;
      }
    },
      async updatePassword() {
        if (!this.isPasswordValid()) {
          alert('Slaptažodis neatitinka gairių.');
          return;
        }
        if (this.newPassword !== this.confirmPassword) {
          alert('Slaptažodžiai nesutampa.');
          return;
        }
        const form = new FormData();
        form.append('oldPassword', this.currentPassword);
        form.append('newPassword', this.newPassword);
        const response = await fetch('http://backend.ogversion.com/account/update', {
          method: 'POST',
          headers: {
            accept: '*/*',
            token: localStorage.getItem('token'),
          },
          body: form
          });
          if (response.status === 400) {
            alert('Neteisingas dabartinis slaptažodis.');
            return;
          } 
        this.$router.push({ name: 'home' });
      },
      isPasswordValid() {
        const passwordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?]).{7,}$/;
        return passwordRegex.test(this.newPassword);
      },
    },
  };
  </script>
  
  <style scoped>
  .password-update-view {
    padding: 20px;
  }
  
  .password-guidelines {
    margin-bottom: 20px;
    background-color: darkorange;
  }
  
  .password-guidelines h3 {
    margin-bottom: 10px;
  }
  
  .password-guidelines ul {
    margin-top: 0;
    padding-left: 20px;
  }
  
  .password-guidelines li {
    margin-bottom: 5px;
  }
  
  .password-update-view form div {
    margin-bottom: 15px;
  }
  
  .password-update-view form label {
    display: block;
    margin-bottom: 5px;
  }
  
  .password-update-view form input[type="password"] {
    width: 100%;
    padding: 8px;
    font-size: 16px;
    border: 1px solid #ccc;
    border-radius: 4px;
  }
  
  .password-update-view form button {
    padding: 8px 16px;
    font-size: 16px;
    background-color: #28a745;
    color: #fff;
    border: none;
    border-radius: 4px;
    cursor: pointer;
  }
  
  .password-update-view form button:hover {
    background-color: #218838;
  }
  </style>
  