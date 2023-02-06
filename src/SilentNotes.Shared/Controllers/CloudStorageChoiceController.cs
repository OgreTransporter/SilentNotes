﻿// Copyright © 2018 Martin Stoeckli.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using CommunityToolkit.Mvvm.DependencyInjection;
using SilentNotes.HtmlView;
using SilentNotes.Services;
using SilentNotes.ViewModels;

namespace SilentNotes.Controllers
{
    /// <summary>
    /// Controller to show a <see cref="CloudStorageChoiceViewModel"/> in an (Html)view.
    /// </summary>
    public class CloudStorageChoiceController : ControllerBase
    {
        private CloudStorageChoiceViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudStorageChoiceController"/> class.
        /// </summary>
        /// <param name="viewService">Razor view service which can generate the HTML view.</param>
        public CloudStorageChoiceController(IRazorViewService viewService)
            : base(viewService)
        {
        }

        /// <inheritdoc/>
        protected override IViewModel GetViewModel()
        {
            return _viewModel;
        }

        /// <inheritdoc/>
        public override void ShowInView(IHtmlViewService htmlViewService, KeyValueList<string, string> variables, Navigation redirectedFrom)
        {
            base.ShowInView(htmlViewService, variables, redirectedFrom);
            _viewModel = new CloudStorageChoiceViewModel(
                Ioc.Default.GetService<INavigationService>(),
                Ioc.Default.GetService<ILanguageService>(),
                Ioc.Default.GetService<ISvgIconService>(),
                Ioc.Default.GetService<IThemeService>(),
                Ioc.Default.GetService<IBaseUrlService>(),
                Ioc.Default.GetService<IStoryBoardService>(),
                Ioc.Default.GetService<ICloudStorageClientFactory>());

            VueBindingShortcut[] shortcuts = new[]
            {
                new VueBindingShortcut(VueBindingShortcut.KeyEscape, nameof(CloudStorageChoiceViewModel.GoBackCommand)),
            };
            VueBindings = new VueDataBinding(_viewModel, View, shortcuts);
            _viewModel.VueDataBindingScript = VueBindings.BuildVueScript();
            VueBindings.StartListening();

            string html = _viewService.GenerateHtml(_viewModel);
            View.HtmlView.LoadHtml(html);
        }
    }
}
