using Microsoft.Extensions.DependencyInjection;
using MyAppToday.Model;
using MyAppToday.Service;
using MyAppToday.ViewModel;
using System.Windows;

namespace MyAppToday.View
{
    /// <summary>
    /// Логика взаимодействия для AssignMastersView.xaml
    /// </summary>
    public partial class AssignMastersView : Window
    {
        private IServiceProvider _serviceProvider;
        private IRequestService _requestService;
        public AssignMastersView(IServiceProvider serviceProvider, IRequestService requestService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _requestService = requestService;
            this.DataContext = _serviceProvider.GetRequiredService<AssignMastersViewModel>();
        }

        private void btnAssignMaster(object sender, RoutedEventArgs e)
        {
            var myAssingMastersViewModel = this.DataContext as AssignMastersViewModel;

            if (myAssingMastersViewModel.SelectedMaster == null || myAssingMastersViewModel.SelectedRequest == null)
            {
                MessageBox.Show("Обязательно выберите значения из выпадающих списков!");
            }
            else
            {
                bool resultAssignMaster = _requestService.CanAssignMaster(myAssingMastersViewModel.SelectedRequest, myAssingMastersViewModel.SelectedMaster);

                if (resultAssignMaster)
                {
                    RequestMasters requestMasters = new RequestMasters()
                    {
                        Comment = myAssingMastersViewModel.Comment,
                        MasterId = myAssingMastersViewModel.SelectedMaster.Id,
                        RequestId = myAssingMastersViewModel.SelectedRequest.Id
                    };

                    _requestService.AssignMaster(requestMasters);
                    MessageBox.Show("Мастер успешно назначен на выбранную заявку");
                }
                else
                {
                    MessageBox.Show("Выбранный мастер уже назначен на выбранную заявку");
                }
            }
        }

        private void btnOpenOperatorView(object sender, RoutedEventArgs e)
        {
            var operatorView = _serviceProvider.GetRequiredService<OperatorView>();
            this.Close();
            operatorView.ShowDialog();
        }
    }
}
