<template>
    <div class="header-bar navbar navbar-expand-lg navbar-light bg-light">
      <button class="navbar-brand home-button btn btn-link" @click="redirectToHome">Digital Inspection Platform</button>
      <div class="ml-auto">
        <button v-if="isTokenSet" class="btn btn-info mr-1" @click="updatePassword">Atnaujinti slaptažodį</button>
        <button v-if="isTokenSet" class="btn btn-primary" @click="logout">Atsijungti</button>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    computed: {
      isTokenSet() {
        return localStorage.getItem('token') !== null;
      },
    },
    methods: {
      redirectToHome() {
        this.$router.push('/').catch(() => {});
      },
      updatePassword() {
        this.$router.push('/password/update').catch(() => {});
      },
      logout() {
        localStorage.removeItem('token');
        // Perform any additional logout actions
        this.$router.go('/login');

      },
    },
  };
  </script>
  
  <style scoped>
  .navbar {
    padding: 0.5rem 1rem;
    display: flex;
    justify-content: space-between;
    align-items: center;
  }
  .navbar button {
    margin-left: 10px;
  }
  

  .home-button {
  display: flex;
  align-items: center;
  justify-content: center;
  position: relative;
  background: white;
  border-radius: 75px;
  font-family: 'Montserrat', sans-serif;
  font-size: 20px;
  font-weight: lighter;
  letter-spacing: 2px;
  transition: 1s box-shadow;
}

.home-button:hover {
  box-shadow: 0 5px 35px 0px rgba(0,0,0,.1);
}

.home-button:hover::before, .home-button:hover::after {
  display: block;
  content: '';
  position: absolute;
  background: #FDA8CF;
  border-radius: 75px;
  z-index: -1;
  animation: 1s clockwise infinite;
}

.home-button:hover:after {
  background: #F3CE5E;
  animation: 2s counterclockwise infinite;
}

@keyframes clockwise {
  0% {
    top: -5px;
    left: 0;
  }
  12% {
    top: -2px;
    left: 2px;
  }
  25% {
    top: 0;
    left: 5px;    
  }
  37% {
    top: 2px;
    left: 2px;
  }
  50% {
    top: 5px;
    left: 0;    
  }
  62% {
    top: 2px;
    left: -2px;
  }
  75% {
    top: 0;
    left: -5px;
  }
  87% {
    top: -2px;
    left: -2px;
  }
  100% {
    top: -5px;
    left: 0;    
  }
}

@keyframes counterclockwise {
  0% {
    top: -5px;
    right: 0;
  }
  12% {
    top: -2px;
    right: 2px;
  }
  25% {
    top: 0;
    right: 5px;    
  }
  37% {
    top: 2px;
    right: 2px;
  }
  50% {
    top: 5px;
    right: 0;    
  }
  62% {
    top: 2px;
    right: -2px;
  }
  75% {
    top: 0;
    right: -5px;
  }
  87% {
    top: -2px;
    right: -2px;
  }
  100% {
    top: -5px;
    right: 0;    
  }
}
  </style>