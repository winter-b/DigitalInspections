<template>
    <div class="container">
    <div class="machine-view">
      <div class="machine-details">
        <div class="machine-info">
          <h2>{{ machine.name }}</h2>
          <div style="display: flex; justify-content: flex-end">
          <p>Atnaujintas: {{ new Date(machine.lastUpdated).toLocaleString() }}</p>
          </div>
          <p>Kategorija: {{ machine.category }}</p>
          <p>Aprašymas: {{ machine.description }}</p>
        </div>
      </div>
      <div class="works-table">
        <h2>Darbai</h2>
        <button @click="addWork()" class="btn btn-primary">Kūrti naują</button>
        <div v-if="works.length > 0">
        <table >
        <thead>
        <tr>
        <th class="hoverable" @click="sortWorks('name')" >Pavadinimas</th>
        <th class="hoverable" @click="sortWorks('category')">Kategorija</th>
        <th class="hoverable" @click="sortWorks('addedBy')">Autorius</th>
        <th class="hoverable" @click="sortWorks('lastUpdated')">Atnaujintas</th>
        <th class="hoverable" @click="sortWorks('nextInspection')">Pabaigti iki</th>
        <th style="text-align: center; ">Veiksmas</th>
        </tr>
        </thead>
          <tbody>
            <tr v-for="work in works" :key="work.id" class="work" :style="getRowStyle(work)">
              <td @click="viewWork(work.id)" class="name-collumn">{{ work.name }}</td>
              <td>{{ work.category }}</td>
              <td>{{ work.addedBy }}</td>
              <td>{{ new Date(work.lastUpdated).toLocaleString() }}</td>
              <td>{{ new Date(work.nextInspection).toLocaleDateString() }}</td>
              <td>
                <button class="btn btn-primary" @click="viewWork(work.id)">Peržiūrėti</button>
                <button class="btn btn-warning" @click="editWork(work.id)">Atnaujinti</button>
                <button v-if="isManager" class="btn btn-danger" @click="deleteWork(work.id)">Ištrinti</button>
              </td>
            </tr>
          </tbody>
        </table>
        <div class="legend" @click="sortWorks('isInspected')">
        <div class="legend-item">
        <div class="legend-color green"></div>
        <div class="legend-label">Žalia: Pabaigta</div>
        </div>
        <div class="legend-item">
        <div class="legend-color red"></div>
        <div class="legend-label">Raudona: Vėluojama</div>
        </div>
        </div>
        </div>
        <p v-else>Darbų nėra</p>

        <div class="pagination">
          <button v-for="page in totalPages" :key="page" @click="goToPage(page)" :class="{ active: page === currentPage }">{{ page }}</button>
        </div>
      </div>
    </div>
    </div>
  </template>
  
  <script>
    import jwtdecode from "jwt-decode";
    export default {
        
    data() {
      return {
        totalPages: 0, // Total number of pages
        currentPage: 1, // Current page number
        worksPerPage: 10, // Number of works to display per page
        sortColumn: '', // Store the selected column name for sorting
        sortDirection: '', // Store the sort direction ('asc' or 'desc')
        machine: {},
        works: [],
        isManager: jwtdecode(localStorage.getItem("token")).role === "Manager",
      };
    },
    mounted() {
      this.fetchMachineData();
      this.fetchWorksData();
      setTimeout(() => {
            this.setTotalCountOfWorks();
        }, 500);
    },
    methods: {
        getRowStyle(work) {
      const nextInspectionDate = new Date(work.nextInspection);
      const currentDate = new Date();
      const isPastDue = nextInspectionDate < currentDate && work.isInspected !== true;
      var style = '';
        if (isPastDue) {
            style = 'background-color: #ff0000';
        }
        else if (work.isInspected === true) {
            style = 'background-color: #00ff00';
        }
      return style;
    },
    async fetchMachineData() {
        const machineId = this.$route.params.id;
        const response = await fetch("http://backend.localhost/Machine?machineId="+ machineId, {
            headers: {
            "accept": "text/plain",
            "token": localStorage.getItem("token")
            }
        }).then((response) => response.json());

        if (response.statuscode === 401) {
          this.$router.push("/login");
        }

        this.machine = {
          name: response.name,
          category: response.category,
          description: response.description,
          lastUpdated: response.lastUpdated,
        };
      },
      async fetchWorksData() {
        const machineId = this.$route.params.id;
        const offset = (this.currentPage - 1) * this.worksPerPage; // Calculate the offset based on the current page
        const limit = this.worksPerPage;
        console.log(limit);
        console.log(offset);
        const response = await fetch(`http://backend.localhost/Machine/Works?machineId=${machineId}&limit=${limit}&offset=${offset}`, {
            headers: {
            "accept": "text/plain",
            "token": localStorage.getItem("token")
            }
        }).then((response) => response.json());
        console.log(response);
        console.log(response.length);
        this.works = response.map(work => ({
        id: work.id,
        name: work.name,
        category: work.category,
        description: work.description,
        addedBy: work.addedBy,
        added: work.added,
        lastUpdated: work.lastUpdated,
        nextInspection: work.nextInspection,
        isInspected: work.isInspected,
        machineId: work.machineId,
        }));
        },
        addWork() {
            console.log("parametras: "  + this.$route.params.id);
            this.$router.push({ name: "workcreate", params: { machineId: this.$route.params.id } });
        },
        viewWork(workId) {
            this.$router.push(`/machine/${this.$route.params.id}/work/${workId}`);
        },
        editWork(workId) {
            this.$router.push(`/machine/work/edit/${workId}`);
        },
        deleteWork(workId) {
            if (confirm("Ar tikrai norite ištrinti darbą?")) {
                fetch(`http://backend.localhost/Machine/Work?workId=${workId}`, {
                    method: "DELETE",
                    headers: {
                        "accept": "text/plain",
                        "token": localStorage.getItem("token")
                    }
                }).then((response) => {
                    //reload page
                    this.fetchWorksData();
                    setTimeout(() => {
                        this.setTotalCountOfWorks();
                    }, 500);
               });
            }

            },
        sortWorks(columnName) {
      if (this.sortColumn === columnName) {
        // If the same column is clicked again, toggle the sort direction
        this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
      } else {
        // If a new column is selected, set the sort column and default sort direction to 'asc'
        this.sortColumn = columnName;
        this.sortDirection = 'asc';
      }

      // Implement your sorting logic here based on the selected column and direction
      this.works.sort((a, b) => {
        // Sort by the selected column
        if (a[columnName] < b[columnName]) {
          return this.sortDirection === 'asc' ? -1 : 1;
        }
        if (a[columnName] > b[columnName]) {
          return this.sortDirection === 'asc' ? 1 : -1;
        }
        return 0;
      });
        },
        goToPage(page) {
        if (page >= 1 && page <= this.totalPages && page !== this.currentPage) {
            this.currentPage = page;
            this.fetchWorksData();
        }
        },
        async setTotalCountOfWorks() {
        const machineId = this.$route.params.id;

        var response = await fetch(`http://backend.localhost/Machine/Works?machineId=${machineId}&limit=100&offset=0`, {
            headers: {
            "accept": "text/plain",
            "token": localStorage.getItem("token")
            }
        }).then((response) => response.json());
        this.totalPages = Math.ceil(response.length / this.worksPerPage);
        },
    },
  };
  </script>
  
  <style scoped>
  .machine-view {
    padding: 20px;
  }
  
  .machine-details {
    margin-bottom: 20px;
  }
  
  .machine-info {
    background-color: #f2f2f2;
    padding: 20px;
    border-radius: 5px;
    border-color: black;
    border-style: solid;
  }
  
  .works-table {
    width: 100%;
  }

.hoverable:hover {
    cursor: pointer;
    background-color: rgb(135, 238, 242);
  }
  
  .add-work-button {
    margin-bottom: 10px;
  }
  
  table {
    width: 100%;
    border-collapse: collapse;
  }
  
  th,
  td {
    padding: 8px;
    text-align: left;
    border-bottom: 1px solid #ddd;
  }
  
  th {
    background-color: #f2f2f2;
  }
  

  .work:hover{
    background-color: #f2f2f2;
  }

  .pagination {
  margin-top: 10px;
  display: flex;
  justify-content: center;
}

.pagination button {
  margin: 0 5px;
  padding: 5px 10px;
  border: 1px solid #ddd;
  background-color: #f2f2f2;
  cursor: pointer;
}

.pagination button.active {
  background-color: #ddd;
}
button {
  margin-top: 10px;
  margin-left: 10px;
  padding: 8px 16px;
  font-size: 16px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}
.name-collumn:hover {
  background-color: #dda;
}
.legend {
  display: flex;
  align-items: center;
  margin-bottom: 10px;
}
.legend-item:hover {
  cursor: pointer;
  background-color: rgb(135, 238, 242);
}
.legend-item {
  display: flex;
  align-items: center;
  margin-right: 20px;
}

.legend-color {
  width: 20px;
  height: 20px;
  margin-right: 5px;
}

.legend-color.green {
  background-color: green;
}

.legend-color.red {
  background-color: red;
}

.legend-label {
  font-size: 16px;
}
  </style>